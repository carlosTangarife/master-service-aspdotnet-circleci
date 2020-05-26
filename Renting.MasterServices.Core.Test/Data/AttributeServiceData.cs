using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Test.Data
{
    public static class AttributeServiceData
    {
        public static IList<Attribute> GetListWithThreeAttributes()
        {
            return new List<Attribute>()
            {
                new Attribute
                {
                    Id = 1,
                    AttributeName = "Estado",
                    Order = 1
                },
                new Attribute
                {
                    Id = 2,
                    AttributeName = "Foto",
                    Order = 2
                },
                new Attribute
                {
                    Id = 3,
                    AttributeName = "Descripcion",
                    Order = 3
                }
            };
        }

        public static IList<Attribute> GetListEmptyAttributes()
        {
            return new List<Attribute>();
        }
    }
}
