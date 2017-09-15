using MagazineApp.Web.Areas.Journalist.Models.PicturesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagazineApp.Web.Areas.Journalist.Models.MagazinesViewModels {
    public class MagazineViewModel {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid? MainPictureId { get; set; }
        public PublisherViewModel Publisher { get; set; }
        public DateTime? PublishDate { get; set; }
        public PictureViewModel MainPicture { get; set; }
    }

    public class PublisherViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}