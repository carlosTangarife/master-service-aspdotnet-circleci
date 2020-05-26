using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Test.Data
{
    public static class VehicleTypeServiceData
    {
        public static IList<VehicleType> GetListVehicleTypeWithThreeElements()
        {
            return new List<VehicleType>
            {
                new VehicleType
                {
                    Id = 1,
                    VehicleTypeName = "Campero"
                },
                new VehicleType
                {
                    Id = 2,
                    VehicleTypeName = "Automovil"
                },
                new VehicleType
                {
                    Id = 3,
                    VehicleTypeName = "TractoCamion"
                }
            };
        }

        public static IList<VehicleType> GetListVehicleTypeEmpty()
        {
            return new List<VehicleType>();
        }
    }
}
