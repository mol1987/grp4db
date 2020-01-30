using MsSqlRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var a = await TestEnvironment();
            var b = await TestEnvVariables();
            await TestRepo();
            Console.WriteLine("press any key to quit");
            Console.ReadKey();
            
        }
        static async Task RunTests()
        {

        }
        /// <summary>
        /// ? physical .env in root folder
        /// </summary>
        /// <returns></returns>
        static async Task<bool> TestEnvironment()
        {
            bool res = Helper.Environment.LoadEnvFile() ? true : false;
            if (res)
            {
                TestTool.PrintSuccess("[Success] Loaded .env file");
            }
            else
            {
                TestTool.PrintFailure("[Error] Failed to load .env file");
            }
            return res;
        }
        /// <summary>
        /// ? All global envs are set
        /// </summary>
        /// <returns></returns>
        static async Task<bool> TestEnvVariables()
        {
            bool res = false;
            // Check if global values are set
            try
            {
                Dictionary<string, bool> envs = new Dictionary<string, bool>();
                List<string> keys = new List<string>()
                {
                    {"host"},{"db"},{"usr"},{"pwd"},{"port"}
                };

                // Run string values as keys and fetch if are any envs set to that key
                keys.ForEach(key => envs.Add(key, Helper.Globals.Exists(key)));

                // Filter values !== true, keeping the false ones
                envs = envs.Where(a => !a.Value).ToDictionary(i => i.Key, i => i.Value);

                if (envs.Count() > 0)
                {
                    res = false;
                }

                foreach (var env in envs)
                {
                    TestTool.PrintFailure("Missing key for {0}\n", env.Key);
                }

            }
            catch (Exception e)
            {
                TestTool.PrintFailure(e.Message);
                res = false;
            }
            if (res)
            {
                TestTool.PrintSuccess("[Success] for TestEnvVariables()\n");
            }
            return res;
        }
        static async Task Test2()
        { // Added by meles
            var repo = new Repository();
            foreach (Orders order in (await repo.GetAllOrdersWithArticles()))
            {
                Console.WriteLine(order.ID);
                Console.WriteLine(order.Orderstatus);
                Console.WriteLine(order.TimeCreated);
                foreach (Articles articleOrderItem in order.Articles)
                {
                    Console.Write(" " + articleOrderItem.Name.Trim(' ') + ",");
                }
            }

            foreach (Articles article in (await repo.GetAllArticlesWithIngredients()))
            {
                Console.Write($"{article.Name}");
                foreach (Ingredients ingredientItem in article.Ingredients)
                {
                    Console.Write(" " + ingredientItem.Name.Trim(' ') + ",");
                }
                Console.WriteLine();
            }
        }
        static async Task TestRepo()
        {
            var repo = new Repository();

            // Example with Print Baked into the Object itself
            List<Articles> articles = new List<Articles>();
            articles = await repo.ListArticles();
            TestTool.PrintSuccess("Got {0} row from Articles\n", articles.Count());
            articles.First().PrintKeys();
            articles.ForEach(article => article.PrintRow());

            //
            List<Employees> employees = await repo.ListEmployees();
            TestTool.PrintSuccess("Got {0} row from Employees\n", employees.Count());
            Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\n", "ID", "Name", "LastName", "Email", "Password");
            employees.ForEach(employee => Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\n", employee.ID, employee.Name, employee.LastName, employee.Email, new string('*', employee.Password.Length)));

            //
            var ingredients = await repo.ListIngredients();
            TestTool.PrintSuccess("Got {0} row from Ingredients\n", employees.Count());
            Console.Write("{0}\t{1}\t{2}\n", "ID", "Name", "Price");
            ingredients.ForEach(ingredient => Console.Write("{0}\t{1}\t{2}\n", ingredient.ID, ingredient.Name, ingredient.Price));

            //
            List<Orders> orders = await repo.ListOrders();
            TestTool.PrintSuccess("Got {0} row from Orders\n", orders.Count());
            Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\n", "ID", "TimeCreated", "Orderstatus", "Price", "CustomerID");
            orders.ForEach(order => Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\n", order.ID, order.TimeCreated, order.Orderstatus, order.Price, order.CustomerID));

            // Add 1 article
            Articles newArticle = new Articles();
            newArticle.Name = "Potatissallad";
            //newArticle.Ingredients.Add(new Ingredients() { Name = "Potatis", Price = 0.0f });
            newArticle.Type = "Sallad";
            newArticle.BasePrice = 79.9f;
            var articleid = await repo.AddArticle(newArticle);
            TestTool.PrintSuccess("Added new article id = {0}", articleid);
            await repo.DeleteArticle(articleid);
            TestTool.PrintSuccess("DELETED article with id = {0}", articleid);
            // Update 1 Article

            //repo.UpdateArticle()


            // Delete 1 Article

        }
    }
    public static class TestTool
    {

        public static void PrintSuccess(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write("{0}\n", text);
            Console.ResetColor();
        }
        public static void PrintSuccess(string text, dynamic arg)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write(text, arg);
            Console.ResetColor();
        }
        public static void PrintFailure(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write("{0}\n", text);
            Console.ResetColor();
        }
        public static void PrintFailure(string text, dynamic arg)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write(text, arg);
            Console.ResetColor();
        }
    }
}
