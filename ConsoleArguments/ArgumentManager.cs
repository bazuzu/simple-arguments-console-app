using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConsoleArguments
{
    public class ArgumentManager
    {
        public ArgumentManager()
        {
            Init(new List<ArgumentItem>());
        }
        public ArgumentManager(List<ArgumentItem> argumentItems)
        {
            if (argumentItems == null) argumentItems = new List<ArgumentItem>();
            Init(argumentItems);
        }

        protected List<ArgumentItem> ArgumentItems { get; set; }

        public void Init(List<ArgumentItem> argumentItems)
        {
            ArgumentItems = argumentItems;
            ExecuteList = new List<ArgumentItem>();
        }

        private List<ArgumentItem> ExecuteList { get; set; }



        public void Execute()
        {
            if (ExecuteList.Count == 0)
            {
                throw new ArgumentManagerException("ExecuteList is empty. Call TryParseArgs(args) before Run().");
            }
            foreach (ArgumentItem argItem in ExecuteList)
            {
                argItem.Execute();
            }
        }


        public bool TryParseArgs(string[] args)
        {
            bool foundArg = false;
            for (int i = 0; i < args.Length; i++)
            {
                foreach (ArgumentItem item in ArgumentItems)
                {
                    if (item.ArgumentName == args[i])
                    {
                        int index = item.ParseAgrs(i, args);
                        if (index < i)
                        {
                            return false;
                        }
                        //throw new ArgumentManagerException("ParseAgrs of ArgumentItem object should return index >= current index.");
                        i = index;

                        foundArg = true;

                        ExecuteList.Add(item);
                        break;
                    }
                }
            }

            return foundArg;
        }


        #region Add ArgumentItem

        public ArgumentManager AddArgumentItem(ArgumentItem argumentItem)
        {
            ArgumentItems.Add(argumentItem);
            return this;
        }

        public ArgumentManager AddArgumentItem(string argumentName, Action execute)
        {
            return this.AddArgumentItem(new ArgumentItem(argumentName, execute, null, null));
        }

        public ArgumentManager AddArgumentItem(string argumentName, Action execute, Func<int, string[], int> parseAgrs = null, string help = null)
        {
            return this.AddArgumentItem(new ArgumentItem(argumentName, execute, parseAgrs, help));
        }

        #endregion






    }
}
