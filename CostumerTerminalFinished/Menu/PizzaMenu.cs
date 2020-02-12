using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loading;
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
            Name = "Pizza Meny";
        }
        
        public async Task Print()
        {
            const int padding = 45;
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.mainMenu);
            PagesList.Add(Globals.exitMenu);
            
            Console.Clear();
            Thread thr = new Thread(new ThreadStart(loading));
            thr.Start();
            int no = 1;
            List<Articles> articles = await General.getArticles();
            List<Articles> pizzas = articles.Where(i => i.Type == "Pizza").ToList();
            thr.Interrupt();
            Thread.Sleep(200);
            thr.Interrupt();
            Console.Clear();
            string s = "";
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pizza meny");
            Console.BackgroundColor = ConsoleColor.Black;
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
            Console.BackgroundColor = ConsoleColor.Blue;
            PagesList.ForEach(x => Console.WriteLine(no++ + ". " + x.Name));
            Console.BackgroundColor = ConsoleColor.Black;
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
        static void loading()
        {
            SpinningText sp = new SpinningText();
            sp.PrintLoop();
        }
    }
}
