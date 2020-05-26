using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Renting.MasterServices.Domain.Entities.Provider
{
    public class Announcement: EntityBase
    {
        [Column(name: "IdAnuncio")]
        [Key]
        public int IdAnnouncement { get; set; }

        [Required]
        [MaxLength(80)]
        [Column(name: "strTitulo")]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        [Column(name: "strDescripcion")]
        public string Description { get; set; }

        [Required]
        [Column(name: "lngOrden")]
        public short Order { get; set; }

        [MaxLength(250)]
        [Column(name: "UrlImagen")]
        public string UrlImage { get; set; }

        [MaxLength(250)]
        [Column(name: "UrlMiniaturaImagen")]
        public string UrlThumbnailImage { get; set; }

        [MaxLength(40)]
        [Column(name: "strDescripcionLlamadaAccion")]
        public string DescriptionCallToAction { get; set; }

        [MaxLength(250)]
        [Column(name: "UrlLlamadaAccion")]
        public string UrlCallToAction { get; set; }

        [Column(name: "logEstado")]
        public bool? State { get; set; }

        [Column(name: "IdImagen")]
        public string ImageId { get; set; }

        [Column(name: "idImagenMiniatura")]
        public string ThumbnailImageId { get; set; }
    }
}
