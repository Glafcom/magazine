using AutoMapper;
using MagazineApp.Common.Extensions;
using MagazineApp.Contracts.DtoModels;
using MagazineApp.Domain.Entities;
using MagazineApp.Domain.Entities.Identity;
using MagazineApp.Web.Areas.Account.Models;
using MagazineApp.Web.Models.ArticlesViewModels;
using MagazineApp.Web.Models.MagazinesViewModels;
using MagazineApp.Web.Models.PicturesViewModels;
using MagazineApp.Web.Models.UsersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Mappings {
    public class WebMappingProfile : Profile {

        public WebMappingProfile() {
            
            #region [ Account Models ]

            CreateMap<RegisterViewModel, UserDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<LoginViewModel, UserDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<UserDto, User>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<User, UserDto>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            #endregion

            #region [ Admin Models ]

            CreateMap<User, Areas.Admin.Models.UsersViewModels.UserViewModel>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<Areas.Admin.Models.UsersViewModels.UserViewModel, User>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();

            #endregion


            #region [ Common Models ]

            CreateMap<Article, ArticleViewModel>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<ArticleViewModel, Article>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<Magazine, MagazineViewModel>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<MagazineViewModel, Magazine>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<Picture, PictureViewModel>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<PictureViewModel, Picture>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<User, UserViewModel>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();
            CreateMap<UserViewModel, User>().IgnoreAllNonExisting().ReverseMap().IgnoreAllNonExisting();


            #endregion

        }




    }
}