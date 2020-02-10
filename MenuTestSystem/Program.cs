using System;
using TypeLib;
using Dapper;
using System.Collections.Generic;
using MsSqlRepo;
using System.Threading.Tasks;
using System.Linq;
using MenuTestSystem.Menu;

namespace MenuTestSystem
{
    class Program
    {
        static List<List<Func<Task<int>>>> routes = new List<List<Func<Task<int>>>>();
        static async Task Main(string[] args)
        {
            var a = await TestEnvironment();
            var b = await TestEnvVariables();

            //MenuCode menuCode = new MenuCode();
            MainMenu menus = new MainMenu();
            await menus.Print();
        }

        

        static Articles ChooseArticle(List<Articles> articles)
        {
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            return articles[choice];
        }
        static void changeIngredient(List<Ingredients> ingredients)
        {
            // if there is a duplicate remove all occurence of that ingredients
            // basicly, if theres already an ingredient of the same name count it as a removed ingredient
            List<Ingredients> originalList = ingredients.GetRange(0, ingredients.Count()-1);
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
}
