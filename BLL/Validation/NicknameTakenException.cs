﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class NicknameTakenException : Exception
    {
        public NicknameTakenException(string mes) : base(mes) { }
    }
}