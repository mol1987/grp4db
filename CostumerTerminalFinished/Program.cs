using System;
using TypeLib;
using Dapper;
using System.Collections.Generic;
using MsSqlRepo;
using System.Threading.Tasks;
using System.Linq;
using BeställningsTerminal.Menu;

namespace BeställningsTerminal
{
    class Program
    {
        static List<List<Func<Task<int>>>> routes = new List<List<Func<Task<int>>>>();
        static async Task Main(string[] args)
        {
            /// Change the Window titles
            Console.Title = "Beställning";
            bool res = Helper.Environment.LoadEnvFile() ? true : false;

            //MenuCode menuCode = new MenuCode();
            MainMenu menus = new MainMenu();
            await menus.Print();
        }
    }
}
