using BepInEx;
using BepInEx.Configuration;

namespace IdleVillager;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static ConfigEntry<bool> HighlightVillagers;
    public static ConfigEntry<bool> HighlightFarms;

    private void Awake()
    {

        HighlightVillagers = Config.Bind("General", "HighlightVillagers", true, "Highlight idle villagers.");
        HighlightFarms = Config.Bind("General", "HighlightGardens", true, "Highlight idle gardens.");

        HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }
}
