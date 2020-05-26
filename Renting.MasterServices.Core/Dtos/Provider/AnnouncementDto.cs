using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Renting.MasterServices.Core.Binders;
using System.ComponentModel.DataAnnotations;

namespace Renting.MasterServices.Core.Dtos.Provider
{
    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public class AnnouncementDto : EntityBase
    {
        public int? IdAnnouncement { get; set; }

        [MaxLength(80)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public short Order { get; set; }

        [MaxLength(250)]
        public string UrlImage { get; set; }

        [MaxLength(250)]
        public string UrlThumbnailImage { get; set; }

        [MaxLength(40)]
        public string DescriptionCallToAction { get; set; }

        [MaxLength(150)]
        public string UrlCallToAction { get; set; }

        public bool? State { get; set; }

        public string ImageId { get; set; }

        public string ThumbnailImageId { get; set; }

    }
}
