using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace BeställningsTerminal.Menu
{
    public class ConfirmOrderPage : IMenu
    {
        public List<IMenu> PagesList { get; set; }
        public string Name { set; get; }

        public ConfirmOrderPage()
        {
            Name = "Bekräfta din beställning";    
        }

        public async Task Print()
        {
            Console.Clear();
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.finalizeOrder);
            PagesList.Add(Globals.mainMenu);
            Globals.basketArticles.Add((Articles)Globals.WorkingArticle.Clone());
            int no = 1;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Din beställning är tillagd");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("-------------\n\n");
            Console.BackgroundColor = ConsoleColor.Blue;
            PagesList.ForEach(x => Console.WriteLine(no++ + ". " + x.Name));
            Console.BackgroundColor = ConsoleColor.Black;

            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            if (PagesList != null)
                await PagesList[choice - 1].Print();
            Console.WriteLine("-------------");
        }
    }
}
