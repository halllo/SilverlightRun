using System;
using System.Collections.Generic;

namespace SilverlightRun
{
    /// <summary>
    /// Abstraction over phone state and storage.
    /// </summary>
    public interface IPhoneService
    {
        bool IsActivated { get; }
        void Store(string key, object value);
        bool HasStored(string key);
        object PopStored(string key);
        object GetStored(string key);
        void WhenDeactivated(Action onDeactivated);
        void WhenClosing(Action onClosing);

        Dictionary<string, object> TemporaryStorage { get; }
    }
}
