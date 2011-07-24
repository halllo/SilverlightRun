using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace SilverlightRun.PhoneSpecific.Storage
{
    /// <summary>
    /// Allows simple settings operations on IS settings.
    /// </summary>
    public class IsolatedValues
    {
        public static void Set<T>(string key, T value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains(key))
                settings.Add(key, value);
            else
                settings[key] = value;
        }

        public static T Get<T>(string key, T defaultValue)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(key))
                return (T)settings[key];
            else
                return defaultValue;
        }

        public static IEnumerable<KeyValuePair<string, T>> All<T>()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            foreach (var setting in settings)
            {
                yield return new KeyValuePair<string, T>(setting.Key, (T)setting.Value);
            }
        }

        public static void Delete(string resource)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Remove(resource);
        }
    }
}
