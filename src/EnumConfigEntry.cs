using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IdleVillager
{

    /// <summary>
    /// Converts an enum to a boolean for the UI.
    /// Useful for converting Bit Flagged enums to config values since the UI cannot handle enums.
    /// </summary>
    /// <typeparam name="T">The enum type that this entry represents.</typeparam>
    internal class EnumConfigEntry<T> where T : System.Enum
    {

		private ConfigEntry ConfigEntry { get; set; }

        public T EnumValue { get; set; }

		public bool Enabled
        {
            get => ConfigEntry.GetBool();
            set => ConfigEntry.Value = value;
        }


        /// <summary>
        /// Creates an EnumConfigEntry, using <paramref name="propertyName"/> for the name.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
		public EnumConfigEntry(ConfigFile config, T value, string propertyName, bool defaultValue)
        {
			ConfigEntry = config.GetEntry<bool>(propertyName, defaultValue);
			EnumValue = value;
		}

		/// <summary>
		/// Creates a Mod ConfigEntry, generating a name based off of the enum values.
		/// </summary>
		/// <param name="config">The mod's ConfigFile</param>
		/// <param name="value">The enum value that this entry represents.</param>
		/// <param name="defaultValue">Indicates if the entry is enabled or disabled by default.</param>
		/// <param name="entryPrefix">The prefix for the config entry name.  If not set, uses the name of the enum.</param>
		public EnumConfigEntry(ConfigFile config, T value, bool defaultValue, string? entryPrefix)
        {

            if(entryPrefix is null)
            {
                entryPrefix = typeof(T).Name;
            }

            ConfigEntry = config.GetEntry<bool>(String.Join("_", entryPrefix, Enum.GetName(typeof(T), value)), defaultValue);

            EnumValue = value;
        }


		/// <summary>
		/// Creates or inits an entry for each of the values of an enum.
		/// </summary>
		/// <param name="config">The ConfigFile to read from or write to.</param>
		/// <param name="entryPrefix">The text to show before the entry.  If null, will use the enum's name.</param>
		/// <param name="defaultValue">The value to default.</param>
		/// <param name="ignoreZeroEnum">If true, will ignore the enum value that is assigned zero.</param>
		/// <returns></returns>
		public static List<EnumConfigEntry<T>> InitEntries(ConfigFile config, string? entryPrefix, bool defaultValue, 
            bool ignoreZeroEnum)
        {
            List<EnumConfigEntry<T>> values = new();

			foreach (T value in Enum.GetValues(typeof(T)))
            {
                if(ignoreZeroEnum && Convert.ToInt32(value) == 0)
                {
                    continue;
                }

                EnumConfigEntry<T> entry = new EnumConfigEntry<T>(config, value, defaultValue, entryPrefix);
                values.Add(entry);
            }

            return values;
        }

        public override string ToString()
        {
            return $"Name: {ConfigEntry.Name} Enum {EnumValue} Enabled: {ConfigEntry.Value}";
        }
    }
}
