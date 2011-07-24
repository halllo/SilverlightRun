using System;
using System.Collections.Generic;
using System.Reflection;

namespace SilverlightRun.Tombstoning
{
    /// <summary>
    /// Saves and loades values of SurvivesRestart and SurvivesTombstoning annotated proerties.
    /// Must be setup with SetupFor for any type using these attributes.
    /// </summary>
    public class TombstoneSurvivalEngine
    {
        private static Dictionary<Type, int> instanceIdIndexes = new Dictionary<Type, int>();

        private TombstoneSurvivalEngine(Type t, object instance, IPhoneService phone)
        {
            if (!instanceIdIndexes.ContainsKey(t)) instanceIdIndexes.Add(t, 0);
            int instanceIndex = instanceIdIndexes[t]++;
            SetupIndexed(instance, instanceIndex.ToString(), phone);
        }

        public static TombstoneSurvivalEngine SetupFor<T>(object instance, IPhoneService phone)
        {
            return new TombstoneSurvivalEngine(typeof(T), instance, phone);
        }

        private void SetupIndexed(object instance, string instanceId, IPhoneService phone)
        {
            SetupLaunchActivate(instance, instanceId, phone);
            SetupClosingDeactivate(instance, instanceId, phone);
        }

        private static void SetupLaunchActivate(object instance, string instanceId, IPhoneService phone)
        {
            if (phone.IsActivated)
                LoadOnActivate(instance, instanceId, phone);
            else
                LoadOnLaunch(instance, instanceId);
        }

        private static void SetupClosingDeactivate(object instance, string instanceId, IPhoneService phone)
        {
            phone.WhenDeactivated(() => SaveOnDeactivate(instance, instanceId, phone));
            phone.WhenClosing(() => SaveOnClosing(instance, instanceId));
        }

        private static void SaveOnDeactivate(object instance, string instanceId, IPhoneService phone)
        {
            ForEachPropertyOn<SurvivesTombstoningAttribute>(instance, (p) => SaveForProperty(instance, instanceId, p, new TombstoneSurvivalStateStorage(phone)));
            ForEachPropertyOn<SurvivesRestartAttribute>(instance, (p) => SaveForProperty(instance, instanceId, p, new TombstoneSurvivalISStorage()));
        }

        private static void SaveOnClosing(object instance, string instanceId)
        {
            ForEachPropertyOn<SurvivesRestartAttribute>(instance, (p) => SaveForProperty(instance, instanceId, p, new TombstoneSurvivalISStorage()));
        }

        private static void LoadOnActivate(object instance, string instanceId, IPhoneService phone)
        {
            ForEachPropertyOn<SurvivesTombstoningAttribute>(instance, (p) => FillForProperty(instance, instanceId, p, new TombstoneSurvivalStateStorage(phone)));
            ForEachPropertyOn<SurvivesRestartAttribute>(instance, (p) => FillForProperty(instance, instanceId, p, new TombstoneSurvivalISStorage()));
        }

        private static void LoadOnLaunch(object instance, string instanceId)
        {
            ForEachPropertyOn<SurvivesRestartAttribute>(instance, (p) => FillForProperty(instance, instanceId, p, new TombstoneSurvivalISStorage()));
        }

        private static void SaveForProperty(object instance, string instanceId, PropertyInfo p, ITombstoneSurvivalStorage storage)
        {
            string key = GetKey(instance, instanceId, p);
            object value = p.GetValue(instance, null);
            storage.Store(key, value);
        }

        private static void FillForProperty(object instance, string instanceId, PropertyInfo p, ITombstoneSurvivalStorage storage)
        {
            string key = GetKey(instance, instanceId, p);
            if (storage.HasStored(key))
            {
                p.SetValue(instance, storage.PopStored(key), null);
            }
        }

        private static void ForEachPropertyOn<T>(object instance, Action<PropertyInfo> eachProp)
        {
            foreach (var property in instance.GetType().GetProperties())
                foreach (var attribute in property.GetCustomAttributes(typeof(T), true))
                    eachProp(property);
        }

        private static string GetKey(object instance, string instanceId, PropertyInfo p)
        {
            return instanceId + "#" + instance.GetType().FullName + "." + p.Name;
        }
    }
}
