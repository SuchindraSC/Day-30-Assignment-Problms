using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class cabInvoiceException : Exception
    {
        public enum ExceptionType
        {
            INVALID_RIDE_TYPE,
            INVALID_DISTANCE,
            INVALID_TIME,
            NULL_RIDES,
            INVALID_USERID
        }
        ExceptionType type;

        public cabInvoiceException(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
