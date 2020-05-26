using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.MasterServices.Infraestructure.CustomExceptions
{
    [Serializable]
    public class InvalidFileExtensionException : Exception
    {
        public InvalidFileExtensionException(string message) : base(message)
        {

        }
    }
}
