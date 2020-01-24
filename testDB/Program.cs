using MsSqlRepo;
using System;
using System.Threading.Tasks;
using TypeLib;

namespace testDB
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var repo = new Repository();

            foreach (Articles article in (await repo.GetAllArticlesWithIngredients()))
            {
                Console.Write($"{article.Name}");
                foreach (Ingredients ingredientItem in article.Ingredients)
                {
                    Console.Write(" " + ingredientItem.Name.Trim(' ') + ",");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Hello World!");
        }
    }
}
