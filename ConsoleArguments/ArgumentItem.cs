using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleArguments
{
    public class ArgumentItem
    {
        public ArgumentItem(string argumentName, Action execute, Func<int, string[], int> parseAgrs = null, string help = null)
        {
            ArgumentName = argumentName;
            Execute = execute;
            Help = help;
            if (parseAgrs == null)
            {
                ParseAgrs = (index, args) => { return index; };
            }
            else
            {
                ParseAgrs = parseAgrs;
            }
        }
        public string ArgumentName { get; private set; }

        public Action Execute { get; private set; }

        /// <summary>
        /// ParseAgrs(int index - current index of args, string[] args - arguments;
        /// return index - resulting current index;
        /// </summary>
        public Func<int, string[], int> ParseAgrs { get; private set; }

        public string Help { get; private set; }
    }
}
