﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class InvalidRegistrationException : Exception
    {
        public InvalidRegistrationException(string mes) : base(mes) { }
    }
}
