using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.Infrastructure.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message)
            :base(message)
        {
            
        }
    }
}
