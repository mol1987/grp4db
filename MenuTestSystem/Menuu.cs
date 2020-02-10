using System;
using System.Collections.Generic;
using System.Text;
using TypeLib;
using MsSqlRepo;
using System.Linq;
using System.Threading.Tasks;

namespace MenuTestSystem
{
    class Menuu
    {
        List<Articles> articles = new List<Articles>();
        List<Articles> pizzas;
        List<Articles> salads;
        List<Articles> basketArticles = new List<Articles>();
        Articles workingArticle = new Articles();
        public int choice;

        public Menuu()
        {
            
        }
        public async Task init()
        {
            //articles = await articles.getArticles();
            pizzas = articles.Where(i => i.Type == "Pizza").ToList();
            salads = articles.Where(i => i.Type == "Salad").ToList();
            Console.WriteLine(pizzas.Count + " " + salads.Count);
        }
        public async Task<int> mainPage()
        {
            Console.WriteLine("Choose a category!");
            Console.WriteLine("1. Pizza");
            Console.WriteLine("2. Salad");
            Console.WriteLine("3. List Orders");
            Console.WriteLine("0. Quit");
            
            int no = 1;
            
            switch (choice)
            {
                case 1:
                    foreach (var item in pizzas)
                    {
                        Console.WriteLine(no++ + " " + item.Name);
                    }
                    workingArticle = ChooseArticle(pizzas);
                    break;
                case 2:
                    foreach (var item in salads)
                    {
                        Console.WriteLine(no++ + " " + item.Name);
                    }
                    workingArticle = ChooseArticle(salads);
                    break;
                case 3:
                    foreach (var item in basketArticles)
                    {
                        Console.WriteLine(no++ + " " + item.Name);
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }
        

        public async Task<int> ingredientsPage()
        {
            bool running = true;
            while (running)
            {
                int no = 0;
                foreach (var item in workingArticle.Ingredients)
                {
                    Console.Write(item.Name + " ");
                }
                Console.WriteLine();
                List<Ingredients> allIngredients = await General.getIngredients();
                foreach (var item in allIngredients)
                {
                    Console.WriteLine(no++ + " " + item.Name);
                }
                int ingredientChoice;
                int.TryParse(Console.ReadLine(), out ingredientChoice);
                if (ingredientChoice <= 0)
                {
                    choice = 0;
                    return 0;
                }
                workingArticle.Ingredients.Add(allIngredients[ingredientChoice-1]);
                changeIngredient(workingArticle.Ingredients);
            }
            return 0;
        }

        public async Task<int> okeyOrder()
        {
            Console.WriteLine("okeyd");
            return 0;
        }

        public async Task<int> seeArticlePage()
        {
            Console.WriteLine(workingArticle.Name);
            int no = 0;

            foreach (var item in workingArticle.Ingredients)
            {
                Console.Write(item.Name + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("---------------");
            Console.WriteLine();
            Console.WriteLine("1. Bekräfta beställning");
            Console.WriteLine("2. Lägg till eller ta bort ingrediens");
            Console.WriteLine("0. Gå tillbaka");
            return 0;
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
