using MsSqlRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace BeställningsTerminal.Menu
{
    public class IngredientsPage : IMenu
    {
        public List<IMenu> PagesList { get; set; }
        public string Name { get; set; }

        public IngredientsPage()
        {
            Name = "Ingredienser";
        }

        public async Task Print()
        {
            Console.Clear();
            Name = "Ingredienser";
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.showArticle);
            int no = 1;
            Console.BackgroundColor = ConsoleColor.Blue;
            foreach (var item in Globals.WorkingArticle.Ingredients)
            {
                Console.Write(item.Name + " ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            List<Ingredients> allIngredients = await General.getIngredients();
            foreach (var item in allIngredients)
            {
                Console.WriteLine(no++ + " " + item.Name);
            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Blue;
            PagesList.ForEach(x => Console.WriteLine(no++ + " " + x.Name));
            Console.BackgroundColor = ConsoleColor.Black;
            int ingredientChoice;
            int.TryParse(Console.ReadLine(), out ingredientChoice);
            if (ingredientChoice < no - 2 && ingredientChoice > 0)
            {
                Globals.WorkingArticle.Ingredients.Add(allIngredients[ingredientChoice - 1]);
                changeIngredient(Globals.WorkingArticle);
            }
            await PagesList[0].Print();
        }

        Articles ChooseArticle(List<Articles> articles)
        {
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            return articles[choice];
        }
        void changeIngredient(Articles a)
        {
            // if there is a duplicate remove all occurence of that ingredients
            // basicly, if theres already an ingredient of the same name count it as a removed ingredient
            List<Ingredients> originalList = a.Ingredients.GetRange(0, a.Ingredients.Count() - 1);
            if (originalList.Count > 0)
            {
                Ingredients found = originalList.Find(x => x.Name == a.Ingredients.Last().Name);
                if (found != null)
                {
                    a.BasePrice -= found.Price;
                    a.Ingredients.RemoveAll(x => x.Name == found.Name);
                } else
                {
                    a.BasePrice += a.Ingredients.Last().Price;
                }
            }
        }
    }
}
