using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdleVillager
{

    [HarmonyPatch(typeof(GameCard), "Update")]
    internal static class GameCardIdle_Patch
    {


        public static void Postfix(GameCard __instance)
        {

            if(__instance.IsDemoCard)
            {
                return;
            }

            //Check for villager not in combat or farm.  Then check if there is a parent or child card attached.
            if (IsVillagerIdle(__instance) || IsFarmIdle(__instance)) 
            {
                __instance.HighlightRectangle.enabled = true;
                __instance.HighlightRectangle.Color = UnityEngine.Color.red;
            }
        }

        /// <summary>
        /// Returns true if the Highlight Villager option is true and the villager is idle.
        /// </summary>
        /// <param name="gameCard"></param>
        /// <returns></returns>
        private static bool IsVillagerIdle(GameCard gameCard)
        {
            return PlugIn.ModConfig.HighlightVillagers && gameCard.CardData is Villager
                && ((Combatable)gameCard.CardData).MyConflict == null
                && gameCard.Parent == null && gameCard.Child == null;
        }

        /// <summary>
        /// Returns true if the Highlight Garden option is true and the farm is idle.
        /// </summary>
        /// <param name="gameCard"></param>
        /// <returns></returns>
        private static bool IsFarmIdle(GameCard gameCard)
        {
            FoodProducers highlight = PlugIn.ModConfig.HighlightFoodProducers.Value;


            bool checkIsIdle = false;

            if (gameCard.CardData is Garden && ((highlight & FoodProducers.FarmsAndGardens) != 0))
            {
                checkIsIdle = true;
            }

            if (gameCard.CardData is FishingSpot && ((highlight & FoodProducers.FishingSpot) != 0))
            {
                checkIsIdle = true;
            }

            if (gameCard.CardData is FishTrap  && ((highlight & FoodProducers.FishingTrap) != 0))
            {
                checkIsIdle = true;
            }

            if (gameCard.CardData is Greenhouse && ((highlight & FoodProducers.Greenhouse) != 0))
            {
                checkIsIdle = true;
            }

            if(checkIsIdle)
            {
                return gameCard.Parent == null && gameCard.Child == null;
            }

            return false;

        }
    }
}
