using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    public class EmailException : CustomException
    {
        public EmailException(string mes) : base(mes) { }
    }
}
