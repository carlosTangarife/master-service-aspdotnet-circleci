using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;

namespace Renting.MasterServices.Core.Test.Data
{
    public static class StateServiceData
    {
        public static IList<State> GetListStateWithThreeElements()
        {
            return new List<State> {
                new State
                {
                    Id = 420,
                    Color = string.Empty,
                    Order = 2,
                    StateName = "Golpe leve",
                    StateType = 39
                },
                new State
                {
                    Id = 421,
                    Color = string.Empty,
                    Order = 3,
                    StateName = "Golpe  Medio",
                    StateType = 39
                },
                new State
                {
                    Id = 422,
                    Color = string.Empty,
                    Order = 4,
                    StateName = "Golpe  Fuerte",
                    StateType = 39
                }
            };

        }

        public static IList<State> GetListEmptyStates()
        {
            return new List<State>();
        }
    }
}
