using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Renting.MasterServices.Domain.Entities.Provider
{
    public class UserProvider : EntityBase
    {
        [Column(name: "idUser")]
        public string Id { get; set; }

        [Column(name: "idProveedor")]
        public int ProviderId { get; set; }

        [Column(name: "strCreadoPor")]
        public string CreatedBy { get; set; }

        [Column(name: "dtmFechaCreacion")]
        public DateTime CreationDate { get; set; }

        [Column(name: "strModificadoPor")]
        public string ModifiedBy { get; set; }

        [Column(name: "dtmUltimaModificacion")]
        public DateTime? LastModification { get; set; }

        [Column(name: "strCorreoUsuario")]
        public string EmailUser { get; set; }
    }
}
