using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TypeLib;
using Dapper;
using System.Threading.Tasks;

namespace MenuTestSystem.Menu
{
    public class MainMenu : IMenu
    {
        public string Name { get; set; }
        int no = 1;
        int choice = 0;
        public List<IMenu> PagesList { get; set; }
        public MainMenu()
        {
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.pizzaMenu);
        }
        public async Task Print()
        {
            Console.Clear();
            Console.WriteLine("Choose a category!");
            PagesList.ForEach(x => Console.WriteLine(no++ + " " + x.Name));
            Console.WriteLine("0. Quit");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice <= 0) return;
            await PagesList[choice-1].Print();
        }
    }
}
