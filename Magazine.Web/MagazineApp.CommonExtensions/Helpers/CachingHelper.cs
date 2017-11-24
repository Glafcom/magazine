using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.CommonExtensions.Helpers {
    public static class CachingHelper {
        private static MemoryCache _cache = new MemoryCache("MagazineAppCache");

        public static object GetItem(string key) {
            return _cache.Get(key);
        }

        public static void Store(string key, object value, DateTimeOffset offset) {
            _cache.Add(new CacheItem(key, value), new CacheItemPolicy() { AbsoluteExpiration = offset });
        }
        
        public static void ClearItem(string key) {
            _cache.Remove(key);
        }
        
    }
}
