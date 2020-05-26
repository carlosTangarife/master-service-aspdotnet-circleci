using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Test.Data
{
    public static class ClientUserServiceData
    {
        public static IList<ClientUser> GetListClientUserWithThreeElements()
        {
            return new List<ClientUser>
            {
                new ClientUser
                {
                    Id = 1,
                    ClientName ="",
                    EconomicGroupId=1,
                    Selected = false
                },
                new ClientUser
                {
                    Id = 2,
                    ClientName ="",
                    EconomicGroupId=1,
                    Selected = false
                },
                new ClientUser
                {
                    Id = 3,
                    ClientName ="",
                    EconomicGroupId=1,
                    Selected = false
                }
            };
        }

        public static IList<ClientUser> GetListEmptyClientUser()
        {
            return new List<ClientUser>();
        }
    }
}
