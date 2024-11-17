using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Interfaces.CachingServices
{
    public interface ICachingServices
    {
        public T GetData<T>(string key);

        public object RemoveData(string key);

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
    }
}
