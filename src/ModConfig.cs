using System;
using System.Collections.Generic;
using System.Text;

namespace IdleVillager
{
	/// <summary>
	/// The configuration settings for the mod
	/// </summary>
	internal class ModConfig
	{

		public bool HighlightVillagers { get; private set; }

		private ConfigEntry HighlightVillagersConfig { get; set; }

		public FoodProducersConfig HighlightFoodProducers { get; private set; }
		
		private ConfigFile Config { get; set; }

        public ModConfig(ConfigFile config)
        {
			Config = config;

			HighlightVillagersConfig = Config.GetEntry<bool>("Highlight Villagers", true);
			HighlightFoodProducers = new FoodProducersConfig(this.Config);

			OnSave();
		}

		/// <summary>
		/// Call when the configuration has been saved to recompute the calculated values.
		/// </summary>
		public void OnSave()
		{
			HighlightFoodProducers.OnSave();
			HighlightVillagers = HighlightVillagersConfig.GetBool();
		}

	}
}
