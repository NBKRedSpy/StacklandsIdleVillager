using BepInEx;
using BepInEx.Configuration;
using System;

namespace IdleVillager;


[Flags]
public enum FoodProducers
{
    None = 0,
    Garden = 0x1,
    FishingSpot = 0x2,
    FishingTrap = 0x4,
    Greenhouse  = 0x8,
}

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{

    public static ConfigEntry<bool> HighlightVillagers;
    public static ConfigEntry<FoodProducers> HighlightFoodProducers;


    private void Awake()
    {

        HighlightVillagers = Config.Bind("General", "HighlightVillagers", true, "Highlight idle villagers.");

        HighlightFoodProducers = Config.Bind("General", "HighlightFoodProducers", 
            FoodProducers.Garden | 
            FoodProducers.FishingSpot | 
            FoodProducers.FishingTrap | 
            FoodProducers.Greenhouse, 
            "The food producers to highlight when idle.  Set to None to not highlight food producers.");


        HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }
}
