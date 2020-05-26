using System.ComponentModel.DataAnnotations.Schema;

namespace Renting.MasterServices.Domain.Entities.Client
{
    public class ClientUser : EntityBase
    {
        [Column(name: "intIdCliente")]
        public int Id { get; set; }

        [Column(name: "strNombreCliente")]
        public string ClientName { get; set; }

        [Column(name: "intIdGrupo")]
        public int EconomicGroupId { get; set; }

        [Column(name: "bitSelected")]
        public bool Selected { get; set; }
    }
}
