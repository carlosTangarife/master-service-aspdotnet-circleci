using System;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class CacheRequestDto : EntityBase
    {
        public string Key { get; set; }

        public object Value { get; set; }
    }
}
