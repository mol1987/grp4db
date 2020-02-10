using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeLib;
using MsSqlRepo;

namespace MenuTestSystem.Menu
{
    public class Reciept : IMenu
    {
        public List<IMenu> PagesList { get; set; }
        public string Name { get; set; }

        public async Task Print()
        {
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
            
            await PagesList[0].Print();
        }
    }
}
