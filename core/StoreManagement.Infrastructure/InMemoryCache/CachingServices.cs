using StoreManagement.Application.Interfaces.CachingServices;
using System.Runtime.Caching;

namespace StoreManagement.Infrastructure.InMemoryCache
{
    public class CachingServices : ICachingServices
    {
        private ObjectCache _memoryCache = MemoryCache.Default;
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T) _memoryCache.Get(key);
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object RemoveData(string key)
        {
            throw new NotImplementedException();
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            throw new NotImplementedException();
        }
    }
}
