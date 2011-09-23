using System;
using System.Collections.Generic;
using Microsoft.Phone.Shell;

namespace SilverlightRun.PhoneSpecific
{
    /// <summary>
    /// Abstracts over PhoneApplicationService to be used with AbstractionHelper. Encapsulates Tombstoning events and state.
    /// </summary>
    public class PhoneServiceAdapter : IPhoneService
    {
        public Dictionary<string, object> TemporaryStorage { get; set; }

        public PhoneServiceAdapter()
        {
            TemporaryStorage = new Dictionary<string, object>();
        }

        public bool IsActivated
        {
            get { return PhoneApplicationService.Current.StartupMode == StartupMode.Activate; }
        }

        public void Store(string key, object value)
        {
            if (!HasStored(key))
                PhoneApplicationService.Current.State.Add(key, value);
            else
                PhoneApplicationService.Current.State[key] = value;
        }

        public bool HasStored(string key)
        {
            return PhoneApplicationService.Current.State.ContainsKey(key);
        }

        public object PopStored(string key)
        {
            var value = PhoneApplicationService.Current.State[key];
            PhoneApplicationService.Current.State.Remove(key);
            return value;
        }

        public object GetStored(string key)
        {
            return PhoneApplicationService.Current.State[key];
        }

        public void WhenDeactivated(Action onDeactivated)
        {
            PhoneApplicationService.Current.Deactivated += (_, __) => onDeactivated();
        }

        public void WhenClosing(Action onClosing)
        {
            PhoneApplicationService.Current.Closing += (_, __) => onClosing();
        }
    }
}
