using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validation.Exceptions
{
    /// <summary>
    /// Throw when nickname is already taken
    /// </summary>
    /// <seealso cref="CustomException" />
    public class NicknameTakenException : CustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NicknameTakenException"/> class.
        /// </summary>
        /// <param name="mes"></param>
        public NicknameTakenException(string mes) : base(mes) { }
    }
}
