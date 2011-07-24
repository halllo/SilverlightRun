
namespace SilverlightRun.Tombstoning
{
    /// <summary>
    /// Abstracts over concrete storage and retrieval of key-value pairs for tombstoning.
    /// </summary>
    public interface ITombstoneSurvivalStorage
    {
        void Store(string key, object value);
        bool HasStored(string key);
        object PopStored(string key);
    }
}
