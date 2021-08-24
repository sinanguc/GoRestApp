namespace GorestApp.Core.Infrastructure.Caching.Microsoft
{
    public interface IMemoryCacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object data, int duration);
        bool IsAdded(string key);
        void Remove(string key);
        void RemoveByPattern(string key);
    }
}
