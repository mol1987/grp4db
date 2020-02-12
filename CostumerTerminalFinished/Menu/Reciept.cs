using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeLib;
using MsSqlRepo;

namespace BeställningsTerminal.Menu
{
    public class Reciept : IMenu
    {
        public List<IMenu> PagesList { get; set; }
        public string Name { get; set; }
        public Reciept()
        {
            Name = "Receipt";
        }
        public async Task Print()
        {
            Name = "Reciept";
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Din order behandlas");
            Console.BackgroundColor = ConsoleColor.Black;
            Orders order = new Orders { CustomerID = 10 };
            order.Articles = Globals.basketArticles;
            await order.insertOrder();
            Console.Clear();
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.mainMenu);
            int no = 1;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tack för din betalning!");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("-------------");
            float totalCost = 0;
            foreach (var item in Globals.basketArticles)
            {
                Console.WriteLine("[x] " + item.Name);
                item.Ingredients.ForEach(x => Console.Write(x.Name + " "));
                totalCost += (float)item.BasePrice;
                Console.WriteLine("\n");
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nTotal Kostnad: " + totalCost + ":-\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("-------------\n");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Your order number is: " + order.ID);
            Console.BackgroundColor = ConsoleColor.Black;
            System.Threading.Thread.Sleep(10000);
            await PagesList[0].Print();
        }
    }
}
