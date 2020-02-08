using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

namespace AdminTerminal
{
    public static class Global
    {
        /// <summary>
        /// Setting false quits the programs immediately
        /// </summary>
        public static bool IsRunning;
        /// <summary>
        /// todo; based on whetever session is active or log-in action === true 
        /// </summary>
        public static bool IsLoggedIn;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Global.IsRunning = true;
            // Load env
            Helper.Environment.LoadEnvFile();
            while (Global.IsRunning)
            {
                try
                {
                    Operation op = new Operation(Console.ReadLine());
                }
                catch (Exception e)
                {
                    //todo; create more specific exceptions
                    Console.WriteLine($"Unsuccesful => {e.Message}, {e.GetType().Name}");
                }
            }
        }
    }
}
