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
            //await TestRepo();
            //await TestRepo2();
            Console.WriteLine("press any key to quit");
            Console.ReadKey();


            Articles art = new Articles { Name = "hej", Ingredients = new List<Ingredients>() };

            Articles hej = art.Clone();
            Console.WriteLine(hej.Name);
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

        static async Task TestRepo2()
        {
            List<Articles> articles = new List<Articles>();
            articles = (await General.articlesRepo.GetAllAsync()).ToList();
            TestTool.PrintSuccess("Got {0} row from Articles\n", articles.Count());

            Orders order = new Orders { CustomerID = 10};
            order.Articles = articles.GetRange(0, 2);
            await order.insertOrder();

            List<Articles> orderListArticles = new List<Articles>();
            orderListArticles = await orderListArticles.getArticlesFromOrder(order);
            foreach (var item in orderListArticles)
            {
               
                Console.WriteLine("\n" +item.Name + " " + item.Type) ;
               
                
                foreach (var item2 in item.Ingredients)
                {
                    Console.Write(item2.Name.Trim() + ", ");
                }
            }
        }

        static async Task TestRepo()
        {
            var repo = new ArticlesRepository("Articles");

            // Example with Print Baked into the Object itself
            List<Articles> articles = new List<Articles>();
            articles = (await repo.GetAllAsync()).ToList();
            TestTool.PrintSuccess("Got {0} row from Articles\n", articles.Count());
            articles.First().PrintKeys();
            articles.ForEach(article => article.PrintRow());

            //
            var repoEmployees = new EmployeesRepository("Employees");
            List<Employees> employees = (await repoEmployees.GetAllAsync()).ToList();
            TestTool.PrintSuccess("Got {0} row from Employees\n", employees.Count());
            Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\n", "ID", "Name", "LastName", "Email", "Password");
            employees.ForEach(employee => Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\n", employee.ID, employee.Name, employee.LastName, employee.Email, new string('*', employee.Password.Length)));

            //
            var repoIngredients = new IngredientsRepository("Ingredients");
            var ingredients = (await repoIngredients.GetAllAsync()).ToList();
            TestTool.PrintSuccess("Got {0} row from Ingredients\n", employees.Count());
            Console.Write("{0}\t{1}\t{2}\n", "ID", "Name", "Price");
            ingredients.ForEach(ingredient => Console.Write("{0}\t{1}\t{2}\n", ingredient.ID, ingredient.Name, ingredient.Price));

            //
            var repoOrders = new OrdersRepository("Orders");
            List<Orders> orders = (await repoOrders.GetAllAsync()).ToList();
            TestTool.PrintSuccess("Got {0} row from Orders\n", orders.Count());
            Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\n", "ID", "TimeCreated", "Orderstatus", "Price", "CustomerID");
            orders.ForEach(order => Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\n", order.ID, order.TimeCreated, order.Orderstatus, order.Price, order.CustomerID));

            //await repoOrders.UpdateAsync(new Orders() {ID = 12, Orderstatus = 0, Price = 124, CustomerID = 1 });

            //await repoIngredients.InsertCustomIngredientsAsync(orders.First(), articles.First(), articles.First().Ingredients);
            Orders order = orders.First() ;
            List<Articles> tempArticles = articles.GetRange(0, 2);
            //await repoOrders.MakeOrderAsync(order, tempArticles);
            
            List<Articles> orderListArticles = (await repo.GetAllAsync(order)).ToList();
            foreach (var item in orderListArticles)
            {
                Console.WriteLine(item.Name + " " + item.Type);
                foreach (var item2 in item.Ingredients)
                {
                    Console.Write(item2.Name.Trim() + ", ");
                }
            }

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
