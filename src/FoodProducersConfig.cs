using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using HarmonyLib.Tools;

namespace IdleVillager
{
	/// <summary>
	/// The settings for highlighting food producers.
	/// </summary>
	// Note - This code is a bit complex because I wanted to keep the flags instead of having each item separate.
	//	Technically checking each bool woud have worked, but wanted to see how bit flags would work with the new mod UI.
	//	Especially since the Mod Settings UI currently only handles, strings, floats, and booleans.
	internal class FoodProducersConfig
	{
		public FoodProducers Value { get; private set; }

		/// <summary>
		/// The boolean version of the FoodProducers since the UI doesn't support enums.
		/// This is only used for config loading.  Use HighlightFoodProducers instead.
		/// </summary>
		private List<EnumConfigEntry<FoodProducers>> FoodProducersEnumEntries;

		public FoodProducersConfig(ConfigFile config)
        {
			var highlights = new List<EnumConfigEntry<FoodProducers>>();

			highlights.Add(new EnumConfigEntry<FoodProducers>(config, FoodProducers.FarmsAndGardens, "Farms and Gardens", true));
			highlights.Add(new EnumConfigEntry<FoodProducers>(config, FoodProducers.FishingSpot, "Fishing Spots", true));
			highlights.Add(new EnumConfigEntry<FoodProducers>(config, FoodProducers.FishingTrap, "Fishing Traps", true));
			highlights.Add(new EnumConfigEntry<FoodProducers>(config, FoodProducers.Greenhouse, "Greenhouses", true));

			FoodProducersEnumEntries = highlights;
			OnSave();
		}

		/// <summary>
		/// Sets the HighlightFoodProducers value based on the boolean ConfigEntries that can be set by the 
		/// Options UI.
		/// </summary>
		private void SetEnumValue()
		{
			Value = 0;
			FoodProducersEnumEntries.ForEach(x => Value |= x.Enabled ? x.EnumValue : 0);
		}

		/// <summary>
		/// Call when the configuration has been saved to recompute the calculated values.
		/// </summary>
		public void OnSave()
		{
			SetEnumValue();
		}

	}
}
