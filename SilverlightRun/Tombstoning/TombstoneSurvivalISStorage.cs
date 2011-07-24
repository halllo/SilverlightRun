using System.IO.IsolatedStorage;

namespace SilverlightRun.Tombstoning
{
    /// <summary>
    /// Allows storage and retrieval of key-value pairs in isolated storage.
    /// </summary>
    public class TombstoneSurvivalISStorage : ITombstoneSurvivalStorage
    {
        private object ISSLoad(string name)
        {
            object thing;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(name)
                && null != (thing = IsolatedStorageSettings.ApplicationSettings[name]))
                return thing;
            else
                return null;
        }

        private void ISSSave(string name, object thing)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(name))
                IsolatedStorageSettings.ApplicationSettings.Add(name, thing);
            else
                IsolatedStorageSettings.ApplicationSettings[name] = thing;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public void Store(string key, object value)
        {
            ISSSave(key, value);
        }

        public bool HasStored(string key)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key);
        }

        public object PopStored(string key)
        {
            return ISSLoad(key);
        }
    }
}
