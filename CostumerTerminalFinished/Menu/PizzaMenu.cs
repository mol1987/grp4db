using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsSqlRepo;
using TypeLib;

namespace BeställningsTerminal.Menu
{
    public class PizzaMenu : IMenu
    {
        public string Name { get; set; }
        public List<IMenu> PagesList { get; set; }
        public PizzaMenu()
        {
            Console.WriteLine("piiiiiiiizamenu");
            Name = "PizzaMenu";
            PagesList = new List<IMenu>();
            Console.WriteLine("Name " + Globals.showArticle.Name);
            PagesList.Add(Globals.showArticle);
        }

        public async Task Print()
        {
            Console.Clear();
            int no = 1;
            List<Articles> articles = await General.getArticles();
            List<Articles> pizzas = articles.Where(i => i.Type == "Pizza").ToList();
            pizzas.ForEach(x => Console.WriteLine(no++ + " " + x.Name));
            
            Console.WriteLine("0. Go back");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            Globals.WorkingArticle = pizzas[choice - 1];
            Console.WriteLine("Count " + Name);
            if (PagesList != null)
                await PagesList[0].Print();
        }
    }
}
