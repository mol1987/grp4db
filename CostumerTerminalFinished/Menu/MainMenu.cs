using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TypeLib;
using Dapper;
using System.Threading.Tasks;

namespace BeställningsTerminal.Menu
{
    public class MainMenu : IMenu
    {
        public string Name { get; set; }
        int no = 1;
        int choice = 0;
        public List<IMenu> PagesList { get; set; }
        public MainMenu()
        {
            Name = "MainMenu";   
        }
        public async Task Print()
        {       
            no = 1;
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.pizzaMenu);
            PagesList.Add(Globals.showOrder);
            PagesList.Add(Globals.exitMenu);
            Console.Clear();
            Console.WriteLine("Välj den typ av mat du vill beställa:");
            Console.WriteLine("----------------\n");
            PagesList.ForEach(x => Console.WriteLine(no++ + ". " + x.Name));
            Console.WriteLine("\n----------------\n");
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice <= 0 || choice > PagesList.Count);
            await PagesList[choice - 1].Print();
        }
    }
}
