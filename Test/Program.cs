using MsSqlRepo;
using System;
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
            int i = 0; // Increment
            int n = 2; // Number of test
            Console.Write("Running {0} tests\n", n);

            //test1 - Check if .env exists and is loaded
            string res = Helper.Environment.LoadEnvFile() ? "true" : "false";
            if (res == "true")
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            Console.Write("Loaded env = {0}\n", res);
            Console.ResetColor();
            i++;

            //test1b Check if global values are set
            try
            {
                // key => value
                string[] values = new string[]
                {
                    "host",Helper.Globals.Get("host"),
                    "db",Helper.Globals.Get("db"),
                    "usr",Helper.Globals.Get("usr"),
                    "pwd",Helper.Globals.Get("pwd"),
                    "port",Helper.Globals.Get("port"),
                };

                for(int j = 0; j < values.Length; j += 2)
                {
                    Console.Write("{0} => {1}\n", values[j], values[j + 1]);
                }
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("Loaded env = {0}\n", res);
                Console.ResetColor();
            }


            //test2
            await Test2();
            i++;

            Console.WriteLine("Press any key to quit..");
            Console.ReadKey();
            System.Environment.Exit(1);
        }

        static async Task Test2()
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
        }
    }
}
