﻿using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Contracts.BLLContracts.Services {
    public interface IArticleService : IBaseService<Article> {
        List<Article> GetArticlesByFilter(ArticleFilter filter);
        List<Article> GetPublishedArticlesByFilter(ArticleFilter filter);
    }
}
