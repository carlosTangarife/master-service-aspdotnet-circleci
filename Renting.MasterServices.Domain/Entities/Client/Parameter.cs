using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Renting.MasterServices.Domain.Entities.Client
{
    public class Parameter : EntityBase
    {
        [Column(name: "iIDParametro")]
        public int Id { get; set; }

        [Column(name: "sNombre")]
        public string Name { get; set; }

        [Column(name: "sDescripcion")]
        public string Description { get; set; }

        [Column(name: "sTipo")]
        public string Type { get; set; }

        [Column(name: "sValor")]
        public string Value { get; set; }

        [Column(name: "sUsuarioIngreso")]
        public string UserLoginName { get; set; }

        [Column(name: "dFechaIngreso")]
        public DateTime LoginDate { get; set; }
    }
}
