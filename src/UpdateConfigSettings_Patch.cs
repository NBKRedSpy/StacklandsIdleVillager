using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace IdleVillager
{
	[HarmonyPatch(typeof(ConfigFile), nameof(ConfigFile.Save))]
	static internal class UpdateConfigSettings_Patch
	{
		static void Postfix(ConfigFile __instance)
		{
			if(__instance.Mod is PlugIn)
			{
				PlugIn.ModConfig.OnSave();
			}
		}
	}
	
}
