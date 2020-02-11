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
            Name = "IngredientsPage";
        }

        public async Task Print()
        {
            Console.Clear();
            Name = "IngredientsPage";
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.showArticle);
            bool running = true;
           
                int no = 1;
                foreach (var item in Globals.WorkingArticle.Ingredients)
                {
                    Console.Write(item.Name + " ");
                }
                Console.WriteLine();
                List<Ingredients> allIngredients = await General.getIngredients();
                foreach (var item in allIngredients)
                {
                    Console.WriteLine(no++ + " " + item.Name);
                }

                PagesList.ForEach(x => Console.WriteLine(no++ + " " + x.Name));
                int ingredientChoice;
                int.TryParse(Console.ReadLine(), out ingredientChoice);
                Globals.WorkingArticle.Ingredients.Add(allIngredients[ingredientChoice - 1]);
                changeIngredient(Globals.WorkingArticle.Ingredients);
                await PagesList[0].Print();
           
        }

        Articles ChooseArticle(List<Articles> articles)
        {
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            return articles[choice];
        }
        void changeIngredient(List<Ingredients> ingredients)
        {
            // if there is a duplicate remove all occurence of that ingredients
            // basicly, if theres already an ingredient of the same name count it as a removed ingredient
            List<Ingredients> originalList = ingredients.GetRange(0, ingredients.Count() - 1);
            if (originalList.Count > 0)
            {
                Ingredients found = originalList.Find(x => x.Name == ingredients.Last().Name);
                if (found != null)
                {
                    //Console.WriteLine(duplicate.Name + " duplicate");
                    ingredients.RemoveAll(x => x.Name == found.Name);
                }
            }
        }
    }
}
