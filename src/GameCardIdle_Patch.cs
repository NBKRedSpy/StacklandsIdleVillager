using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdleVillager
{

    [HarmonyPatch(typeof(GameCard), "Update")]
    public static class GameCardIdle_Patch
    {

        public static void Postfix(GameCard __instance)
        {
            if (__instance.CardData is Villager && __instance.Parent == null && __instance.Child == null && ((Combatable)__instance.CardData).MyConflict == null)
            {
                __instance.HighlightRectangle.enabled = true;
                __instance.HighlightRectangle.Color = UnityEngine.Color.red;
            }

        }
    }
}
