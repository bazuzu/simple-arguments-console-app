using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleArguments
{
    public class ArgumentManagerException : Exception
    {
        public ArgumentManagerException(string message)
            : base(message)
        {
        }
    }
}
