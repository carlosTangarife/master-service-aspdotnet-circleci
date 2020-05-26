using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Renting.MasterServices.Domain.Entities.Client
{
    public class EconomicGroup : EntityBase
    {
        [Column(name: "lngCodGrupoEconomico")]
        public int Id { get; set; }

        [Column(name: "strGrupoEconomico")]
        public string EconomicGroupName { get; set; }

        [Column(name: "bitSelected")]
        public bool Selected { get; set; }
    }
}
