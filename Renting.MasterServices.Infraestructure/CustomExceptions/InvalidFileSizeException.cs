using System;

namespace Renting.MasterServices.Infraestructure.CustomExceptions
{
    [Serializable]
    public class InvalidFileSizeException : Exception
    {
        public InvalidFileSizeException(string message) : base(message)
        {

        }
    }
}
