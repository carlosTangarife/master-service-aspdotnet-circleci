using Renting.MasterServices.Core.Dtos.Client;
using Renting.MasterServices.Domain.Entities.Client;
using System;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Test.Data
{
    public static class PlateServiceData
    {
        public static IList<Plate> GetListPlateWithThreeElements()
        {
            return new List<Plate>
            {
                new Plate
                {
                    PlateCode = "CMN261",
                    Brand = "Chevrolet",
                    Description = "Luv 2.5 Diesel 4x2 - Chasís",
                    DescriptionLarge = "Chevrolet Luv 2.5 Diesel 4x2 - Chasís -Pickup",
                    VehicleTpe = "Pickup"
                },
                new Plate
                {
                    PlateCode = "SNZ295",
                    Brand = "Hino",
                    Description = "Dutro Max Euro IV",
                    DescriptionLarge = "Hino Dutro Max Euro IV -Camión Liviano",
                    VehicleTpe = "Camión Liviano"
                },
                new Plate
                {
                    PlateCode = "SNZ296",
                    Brand = "Hino",
                    Description = "Dutro Max Euro IV",
                    DescriptionLarge = "Hino Dutro Max Euro IV -Camión Liviano",
                    VehicleTpe = "Camión Liviano"
                }
            };
        }

        public static PlateKmRequestDto GetPlateKmRequestDto()
        {
            return new PlateKmRequestDto
            {
                PlateCode = "MNB567",
                LastCounter = 123,
                RouteCentury = 12.5M,
                CounterType = 1,
                CounterDate = DateTime.Now
            };
        }
    }
}
