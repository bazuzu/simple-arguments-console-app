using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleArguments;

namespace simple_arguments_console_app
{
    class Program
    {
        public struct MainArgsContext
        {
            public bool Help;
            public bool Date;
            public int Year;
            public int Month;

            public void WriteToConsole()
            {
                Console.WriteLine("h = {0}, d = {1} (month = {3}, year = {2} )", Help, Date, Year, Month);
            }
        }

        static void Main(string[] args)
        {
            ArgumentManager argManager = new ArgumentManager();

            MainArgsContext context = new MainArgsContext();


            argManager.AddArgumentItem("/h", () => { context.Help = true; })
                      .AddArgumentItem("/date", () => { context.Date = true; }, 
                                        (index, args2) => {

                                            if (args2.Length < index + 2) return -1;
                                            if (!int.TryParse(args2[++index], out context.Year))
                                            {
                                                return -1;
                                            }

                                            if (!int.TryParse(args2[++index], out context.Month))
                                            {
                                                return -1;
                                            }

                                            return index; 
                                        },
                                        null);

            string[] s = new string[] { "/date", "2012", "1" };

            if (argManager.TryParseArgs(s))
            {
                Console.WriteLine("TryParseArgs = true");
                argManager.Execute();
            }

            context.WriteToConsole();

            Console.ReadLine();
            
        }
    }
}
