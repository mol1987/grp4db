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
            Console.WriteLine("Din order behandlas");
            Orders order = new Orders { CustomerID = 10 };
            order.Articles = Globals.basketArticles;
            await order.insertOrder();
            Console.Clear();
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.mainMenu);
            int no = 1;
            Console.WriteLine("Tack för din betalning!");
            Console.WriteLine("-------------");
            foreach (var item in Globals.basketArticles)
            {
                Console.WriteLine(item.Name);
                Globals.WorkingArticle.Ingredients.ForEach(x => Console.Write(x.Name + " "));
                Console.WriteLine();
            }
            Console.WriteLine("-------------");
            Console.WriteLine("Your order number is: " + order.ID);
            System.Threading.Thread.Sleep(5000);
            await PagesList[0].Print();
        }
    }
}
