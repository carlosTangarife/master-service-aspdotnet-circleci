using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Test.Data
{
    public static class EconomicGroupServiceData
    {
        public static IList<EconomicGroup> GetListEconomicGroupWithThreeElements()
        {
            return new List<EconomicGroup>
            {
                new EconomicGroup
                {
                    Id = 1,
                    EconomicGroupName ="",
                    Selected = false
                },
                new EconomicGroup
                {
                    Id = 2,
                    EconomicGroupName ="",
                    Selected = false
                },
                new EconomicGroup
                {
                    Id = 3,
                    EconomicGroupName ="",
                    Selected = false
                }
            };
        }

        public static IList<EconomicGroup> GetListEmptyEconomicGroup()
        {
            return new List<EconomicGroup>();
        }
    }
}
