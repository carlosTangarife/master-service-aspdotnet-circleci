using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Api.Auth
{
    /// <summary>
    /// AzureB2CKeyValidationException
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class AzureB2CKeyValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureB2CKeyValidationException"/> class.
        /// </summary>
        public AzureB2CKeyValidationException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureB2CKeyValidationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AzureB2CKeyValidationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureB2CKeyValidationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AzureB2CKeyValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureB2CKeyValidationException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected AzureB2CKeyValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
