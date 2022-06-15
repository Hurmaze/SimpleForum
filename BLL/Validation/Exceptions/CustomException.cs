using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException() { }

        public CustomException(string message) : base(message)
        {

        }
    }
}
