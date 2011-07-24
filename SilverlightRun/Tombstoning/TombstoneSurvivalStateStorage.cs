
namespace SilverlightRun.Tombstoning
{
    /// <summary>
    /// Allows storage and retrieval of key-value pairs in the phone tombstone state.
    /// </summary>
    public class TombstoneSurvivalStateStorage : ITombstoneSurvivalStorage
    {
        IPhoneService _phone;

        public TombstoneSurvivalStateStorage(IPhoneService phone)
        {
            _phone = phone;
        }

        public void Store(string key, object value)
        {
            _phone.Store(key, value);
        }

        public bool HasStored(string key)
        {
            return _phone.HasStored(key);
        }

        public object PopStored(string key)
        {
            return _phone.PopStored(key);
        }
    }
}
