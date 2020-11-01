using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGeneratorProgram
{
    public class CabInvoiceException : Exception
    {
        /// <summary>
        /// Custom Exception types
        /// </summary>
        public enum Type
        {
            INVALID_RIDE_TYPE,
            INVALID_DISTANCE,
            INVALID_TIME,
            NULL_RIDES,
            INVALID_USER_ID
        }
        public Type type;

        /// <summary>
        /// Constructor for exceptions
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public CabInvoiceException(Type type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
