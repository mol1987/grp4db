using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeställningsTerminal.Menu
{
    public class FinalizeOrder : IMenu
    {
        public List<IMenu> PagesList { get; set; }
        public string Name { get; set; }
        public FinalizeOrder()
        {
            Name = "FinalizeOrder";
        }
        public async Task Print()
        {
            Console.Clear();
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.reciept);
            PagesList.Add(Globals.mainMenu);
            int no = 1;
            Console.WriteLine("Din beställning");
            Console.WriteLine("-------------\n");
            float totalCost = 0;
            foreach (var item in Globals.basketArticles)
            {
                Console.WriteLine("[x] " + item.Name);
                item.Ingredients.ForEach(x => Console.Write(x.Name + " "));
                totalCost += (float)item.BasePrice;
                Console.WriteLine("\n");
            }
            Console.WriteLine("\nTotal Kostnad: " + totalCost + ":-\n");

            Console.WriteLine("-------------");
            PagesList.ForEach(x => Console.WriteLine(no++ + ". " + x.Name));

            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            if (PagesList != null)
                await PagesList[choice - 1].Print();
            Console.WriteLine("-------------");
        }
    }
}
