using MagazineApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.ContractsExtension.BLLContracts {
    public interface ICachingService {
        bool IsArticleInCache(Guid articleId);
        Article FetchArticleFromCache(Guid articleId);
        void AddArticleToCache(Article article);
    }
}
