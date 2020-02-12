using MsSqlRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace BeställningsTerminal.Menu
{
    public class  DrynkMeny : IMenu
    {
        private object drynks;

        public string Name { get; set; }
        public List<IMenu> PagesList { get; set; }
        public DrynkMeny()
        {
            Name = "Drynk Meny";
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
            List<Articles> drynks = articles.Where(i => i.Type == "drynk").ToList();
            string s = "";
            Console.WriteLine("Dryck meny");
            Console.WriteLine("---------------\n");
            foreach (var drynk in drynks)
            {
                string ArticlesString = "" + no++ + ". " + drynk.Name + " (";
                drynk.Ingredients.ForEach(x => ArticlesString += x.Name + ", ");
                ArticlesString = ArticlesString.Substring(0, ArticlesString.Length - 2);
                ArticlesString += " " + s.PadLeft(padding - ArticlesString.Length) + drynk.BasePrice + ":-";
                Console.WriteLine(ArticlesString);
            }
            Console.WriteLine("\n---------------\n");
            PagesList.ForEach(x => Console.WriteLine(no++ + ". " + x.Name));
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice < 1 || choice > no);
            if (choice > 0 && choice < drynks.Count)
            {
                Globals.WorkingArticle = drynks[choice - 1];
                await Globals.showArticle.Print();
            }
            if (PagesList != null)
                await PagesList[choice - (no - PagesList.Count)].Print();
        }
    }
}
