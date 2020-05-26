using System;

namespace Renting.MasterServices.Infraestructure.CustomExceptions
{
    [Serializable]
    public class InvalidFileDimensionException : Exception
    {
        public InvalidFileDimensionException(string message) : base(message)
        {

        }
    }
}

