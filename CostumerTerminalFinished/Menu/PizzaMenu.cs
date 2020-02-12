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
            Name = "PizzaMenu";
        }
        
        public async Task Print()
        {
            const int padding = 45;
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.mainMenu);
            PagesList.Add(Globals.exitMenu);
            Console.Clear();
            int no = 1;
            List<Articles> articles = await General.getArticles();
            List<Articles> pizzas = articles.Where(i => i.Type == "Pizza").ToList();
            string s = "";
            Console.WriteLine("Pizzameny");
            Console.WriteLine("---------------\n");
            foreach (var pizza in pizzas)
            {
                string ingredientsString = "" + no++ + ". " + pizza.Name + " (";
                pizza.Ingredients.ForEach(x => ingredientsString += x.Name + ", ");
                ingredientsString = ingredientsString.Substring(0, ingredientsString.Length - 2);
                ingredientsString += ")" + s.PadLeft(padding - ingredientsString.Length) + pizza.BasePrice + ":-";
                Console.WriteLine(ingredientsString);
            }
            Console.WriteLine("\n---------------\n");
            PagesList.ForEach(x => Console.WriteLine(no++ + ". " + x.Name));
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice < 1 || choice > no);
            if (choice > 0 && choice < pizzas.Count)
            {
                Globals.WorkingArticle = pizzas[choice - 1];
                await Globals.showArticle.Print();
            }
            if (PagesList != null)
                await PagesList[choice - (no - PagesList.Count)].Print();
        }
    }
}
