﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException(string mes) : base(mes) { }
    }
}