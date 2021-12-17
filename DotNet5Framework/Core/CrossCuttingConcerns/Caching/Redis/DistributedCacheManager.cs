using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    class DistributedCacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;

        public DistributedCacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void Add(string key, object data, int duration)
        {
            _cache.SetString(key, JsonConvert.SerializeObject(data), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(duration)
            });
        }

        public T Get<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(_cache.GetString(key));
        }

        public object Get(string key)
        {
            return JsonConvert.DeserializeObject<object>(_cache.GetString(key));
        }

        public bool IsAdd(string key)
        {
            if (string.IsNullOrEmpty(_cache.GetString(key)))
                return false;
            return true;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            // Çalışmayabilir. MemryCacheManager'daki şekilde yapılması gerek.
            Remove(pattern);
        }
    }
}
