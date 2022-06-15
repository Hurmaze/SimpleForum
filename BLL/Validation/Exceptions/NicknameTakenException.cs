using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    public class NicknameTakenException : CustomException
    {
        public NicknameTakenException(string mes) : base(mes) { }
    }
}
