using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Renting.MasterServices.Domain.Entities.Client
{
    public class Attribute : EntityBase
    {
        [Column(name: "lngIdAtributo")]
        public int Id { get; set; }

        [Column(name: "strNombreAtributo")]
        public string AttributeName { get; set; }

        [Column(name: "lngOrden")]
        public short Order { get; set; }
    }
}
