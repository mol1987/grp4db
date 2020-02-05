using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using TypeLib;

namespace AdminTerminal
{
    public class Operation
    {
        /// <summary>
        /// Morphing string
        /// </summary>
        private string Input;
        /// <summary>
        /// Copy of original Readline()
        /// </summary>
        private string OriginalInput;
        private string Command;
        private string Resource;

        public Operation(string input)
        {
            Input = input;
            OriginalInput = input; // Kept for future comparisons/checks
            //if (!res)
            //{
            //    throw new Exception("Server connection error, try again later or contact system adminstrator");
            //}

            Input = Input.TrimStart();
            List<Match> parameters = new List<Match>();
            List<Match> quotatedParameters = new List<Match>();
            List<Match> flags = new List<Match>();
            Match command = Constant.GetRegexByKey("command").Match(Input);
            Match resource = Constant.GetRegexByKey("resource").Match(Input);

            // If -help flag is found
            if (Constant.HasHelpFlag(flags))
            {
                View.WriteLine(Constant.HelpString);
                return; // Break since we don't want to do anything else
            }
            if (!command.Success)
            {
                throw new Exception("no such command");
            }
            if (!resource.Success)
            {
                throw new Exception("no such resource");
            }

            // Clean out the input for remaining parameters
            Input = Input.Remove(command.Index, command.Length);
            Input = Input.Remove((resource.Index - command.Length), resource.Length);
            Input = Input.TrimStart();

            // Uniform casing
            this.Command = command.Value.FirstCharToUpper();
            this.Resource = resource.Value.FirstCharToUpper();

            // Values inside ""-quotes are to be kept together
            if (Input.Contains('"') || Input.Contains('\'') || Input.Contains('`'))
            {
                //todo; match inside
            }

            // Clear out flags from string
            flags = Constant.GetRegexByKey("flags").Matches(Input).ToList();
            int n = 0;
            for (int i = 0; i < flags.Count; i++)
            {
                var flag = flags[i];
                Input = Input.Remove(flag.Index - n, flag.Length);

                n += flag.Length;
            }
            Input = Input.Trim();

            // Parse the rest into parameters,(regex = any non-whitespace character)
            parameters = new Regex(@"\S+").Matches(Input).ToList();

            // Test, Remove
            var test = new TestMe();

            // todo;(?)
            Type ResourceClass = Type.GetType(resource.Value);

            // If all is well, execute dynamically
            Action action = new Action();
            string methodname = this.Command + this.Resource;
            // todo; gör om. Exceptions verkar krångla med .Invoke()
            action.GetType().GetMethod(methodname).Invoke(action, new[] { parameters });

        }
    }
    public class TestMe
    {
        public TestMe()
        {
            throw new Exception("TESTTEST");
        }
    }
    public static class View
    {
        public static void WriteLine(string str)
        {
            Console.WriteLine(str);
        }
        public static bool Success() => true;
        public static bool Error() => false;
        public static void TrowError(string str)
        {
            throw new Exception(str);
        }
    }
    public class Action
    {
        public async void AddArticle(List<Match> p)
        {
            // string name, string baseprice, string type, [..ingredients]
            Articles newArticle = new Articles();
            List<Ingredients> newIngredients = new List<Ingredients>();
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");

            if (p.Count < 4)
            {
                throw new TargetInvocationException(new Exception(String.Format("Missing {0} parameter[s]", 4 - p.Count)));
                //throw new Exception(String.Format("Missing {0} parameter[s]", 4 - p.Count));
            }

            newArticle.Name = p[0].Value;
            float basePrice;
            bool isFloat = float.TryParse(p[1].Value, out basePrice);
            if (!isFloat)
            {
                throw new Exception("Baseprice must be float");
            }
            newArticle.BasePrice = basePrice;
            newArticle.Type = p[2].Value;
            List<string> incomingingredients = p[3].Value.Split(',').ToList();
            incomingingredients.FilterEmpty();
            incomingingredients = incomingingredients.Select(a => a = a.FirstCharToUpper()).ToList();

            // Hämta ner existerande ingredients och jämför om de redan finns
            var existingingredients = (await repo.GetAllAsync()).ToList();
            foreach (var item in existingingredients)
            {
                if (incomingingredients.Contains(item.Name))
                {
                    newIngredients.Add(item);
                }
            }

            if (newIngredients.Count != incomingingredients.Count)
            {
                throw new Exception("Missing ingredient. 'Add Ingredints x first' todo; specify which missing");
            }

            View.WriteLine("Added 1 article");
        }
        public void DeleteArticle(List<Match> p) => View.WriteLine("2");
        public async void ListArticle(List<Match> p)
        {
            List<Articles> articles = new List<Articles>();
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            articles = (await repo.GetAllAsync()).ToList();
            articles.ForEach(article => article.PrintRow());
        }
        public void EditArticle(List<Match> p) => View.WriteLine("4");
        public void AddItem(List<Match> p) => View.WriteLine("5");
        public void DeleteItem(List<Match> p) => View.WriteLine("6");
        public void ListItem(List<Match> p) => View.WriteLine("7");
        public void EditItem(List<Match> p) => View.WriteLine("8");
        public void AddOrder(List<Match> p) => View.WriteLine("9");
        public void DeleteOrder(List<Match> p) => View.WriteLine("10");
        public void ListOrder(List<Match> p) => View.WriteLine("11");
        public void EditOrder(List<Match> p) => View.WriteLine("12");
        public void AddEmployee(List<Match> p) => View.WriteLine("13");
        public void DeleteEmployee(List<Match> p) => View.WriteLine("14");
        public void ListEmployee(List<Match> p) => View.WriteLine("15");
        public void EditEmployee(List<Match> p) => View.WriteLine("16");

        // Constant.Flag.Callbacks
        public string HelpFlag(string str)
        {
            Console.WriteLine("Help flag detected, do something here");
            return "";
        }
        public string HelpFlag(string cmd, string res)
        {
            Console.WriteLine($"Help flag for {cmd} {res} [..]");
            return "";
        }
    }
    /// <summary>
    /// Custom Extensions
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="master"></param>
        /// <returns></returns>
        public static bool ContainsItemFrom(this List<string> source, List<string> master)
        {
            bool result = false;
            foreach (string a in master)
            {
                bool r = source.Contains(a);
                if (r)
                {
                    result = r;
                    break;
                }
            }
            return result;
        }
        public static dynamic Shift<T>(this List<T> list)
        {
            if (list.Count < 1)
            {
                return list;
            }
            var firstitem = list.First();
            list.RemoveAt(0);
            return firstitem;
        }
        /// <summary>
        /// Get what elements are lacking
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static List<T> Missing<T>(this List<T> list, List<T> target)
        {
            List<T> differencial = new List<T>();
            differencial = list.Where(a => !target.Contains(a)).ToList();
            return differencial;
        }
        /// <summary>
        /// Remove empty elements after Trim()
        /// </summary>
        /// <param name="list"></param>
        public static void FilterEmpty(this List<string> list)
        {
            list = list.Where(el => el.Trim().Length > 0).ToList();
        }
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };
    }
}
