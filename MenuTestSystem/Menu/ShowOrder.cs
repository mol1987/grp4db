using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MenuTestSystem.Menu
{
    public class ShowOrder : IMenu
    {
        public List<IMenu> PagesList { get; set; }
        public string Name { get; set; }
        public ShowOrder()
        {
            Name = "ShowOrder";
        }
        public async Task Print()
        {
            Console.Clear();
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.finalizeOrder);
            PagesList.Add(Globals.mainMenu);
            int no = 1;
            Console.WriteLine("Din beställning:");
            Console.WriteLine("-------------");
            foreach (var item in Globals.basketArticles)
            {
                Console.WriteLine(item.Name);
                item.Ingredients.ForEach(x => Console.Write(x.Name + " "));
                Console.WriteLine();
            }
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

