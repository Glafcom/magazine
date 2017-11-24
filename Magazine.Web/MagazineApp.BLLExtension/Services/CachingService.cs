using MagazineApp.BLLExtension.Helpers;
using MagazineApp.ContractsExtension.BLLContracts;
using MagazineApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.BLLExtension.Services {
    public class CachingService : ICachingService {
        public bool IsArticleInCache(Guid articleId) {
            var cache = MemoryCache.Default;
            return cache.Contains($"ar_{articleId.ToString()}");            
        }

        public Article FetchArticleFromCache(Guid articleId) {
            var cache = MemoryCache.Default;
            return (Article)cache[$"ar_{articleId.ToString()}"];            
        }

        public void AddArticleToCache(Article article) {
            var cache = MemoryCache.Default;
            cache.Set($"ar_{article.Id.ToString()}", article, DateTimeOffset.Now.AddHours(2));
        }
    }
}
