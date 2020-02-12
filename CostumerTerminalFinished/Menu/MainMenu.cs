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
            Name = "Huvud meny";   
        }
        public async Task Print()
        {
            no = 1;
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.pizzaMeny);
            PagesList.Add(Globals.DrynkMeny);
            PagesList.Add(Globals.SaladMenu);
            PagesList.Add(Globals.showOrder);
            PagesList.Add(Globals.exitMenu);
            //Globals.pizzaMeny.Name = "ändra meny namn";
            Console.Clear();
            Console.WriteLine("Vällkomen till Pizza Palatset\n");
            Console.WriteLine("välj en av alternativen:");
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
