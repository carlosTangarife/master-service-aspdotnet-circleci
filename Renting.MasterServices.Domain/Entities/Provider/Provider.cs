using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Renting.MasterServices.Domain.Entities.Provider
{
    public class Provider : EntityBase
    {
        [Column(name: "IdProveedor")]
        public int Id { get; set; }

        [Column(name: "strNombreProveedor")]
        public string ProviderName { get; set; }

        [Column(name: "strNitProveedor")]
        public string NitProvider { get; set; }
    }
}
