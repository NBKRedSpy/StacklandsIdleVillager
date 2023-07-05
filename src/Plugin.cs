using System.IO;
using UnityEngine;
using UnityEngineInternal;

namespace IdleVillager;

public class PlugIn : Mod
{

    internal static ModConfig ModConfig { get; private set; } = null!;

    internal static ModLogger ModLogger = null!;
    public override void Ready()
    {
		ModLogger = Logger;
        ModConfig = new ModConfig(Config);
        Config.OnSave += ModConfig.OnSave;
        
        Harmony.PatchAll();
    }

}
