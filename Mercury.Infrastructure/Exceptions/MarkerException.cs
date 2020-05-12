using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.Infrastructure.Exceptions
{
    public class MarkerException : Exception
    {
        public MarkerException(string message)
            :base(message)
        {

        }
    }
}
