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

            Input = Input.TrimStart();
            //List<Match> parameters = new List<Match>();
            List<Match> quotatedParameters = new List<Match>();
            List<Match> flags = new List<Match>();
            Match command = new Regex("(?<command>)^[Aa]dd|^[Dd]elete|^[Uu]pdate|^[Ll]ist").Match(Input);
            Match resource = new Regex("(?<resource>)[Aa]rticles?|[Ee]mployees?|[Ii]ngredients?").Match(Input);

            // If clear command is run
            if (Input == "cls")
            {
                Console.Clear();
                return;
            }
            // If -help flag is found
            if (new Regex("-help|--help|-h").Match(Input).Success)
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

            // ? 'S' as last char, remove it
            if (this.Resource.Last() == 's' || this.Resource.Last() == 'S')
            {
                this.Resource = this.Resource.Remove(this.Resource.Length - 1);
            }

            // Values inside ""-quotes are to be kept together
            if (Input.Contains('"') || Input.Contains('\'') || Input.Contains('`'))
            {
                //todo; match inside
            }

            // Clear out flags from string
            flags = new Regex("(?<flag>-?-\\w+)").Matches(Input).ToList();
            int n = 0;
            for (int i = 0; i < flags.Count; i++)
            {
                var flag = flags[i];
                Input = Input.Remove(flag.Index - n, flag.Length);

                n += flag.Length;
            }
            Input = Input.Trim();

            // Parse the rest into parameters,(regex = any non-whitespace character)
            // Also divided into groups with detection logic
            // variable         = unnamed
            // name=variable    = named
            // " variable "     = literal
            Regex a = new Regex("(?<literal>\"(.*?)\"+)|(?<named>\\S+=\\S+)|(?<unnamed>\\S+)");
            MatchCollection mc = a.Matches(input);
            List<Parameter> parameters = new List<Parameter>();
            foreach (Match m in mc)
            {
                string group = "unknown";
                if (m.Groups["literal"].Success)
                {
                    group = "literal";
                }
                else if (m.Groups["named"].Success)
                {
                    group = "named";
                }
                else if (m.Groups["unnamed"].Success)
                {
                    group = "unnamed";
                }
                parameters.Add(new Parameter(group, m.Groups[group].Value));
            }

            // Joined for semantical pathing
            string methodname = this.Command + this.Resource;

            //..
            var endpoint = new EndPoint();

            switch (methodname)
            {
                case "AddArticle":
                    endpoint.AddArticle(parameters);
                    break;
                case "DeleteArticle":
                    endpoint.DeleteArticle(parameters);
                    break;
                case "ListArticle":
                    endpoint.ListArticle(parameters);
                    break;
                case "UpdateArticle":
                    endpoint.UpdateArticle(parameters);
                    break;
                case "AddEmployee":
                    endpoint.AddEmployee(parameters);
                    break;
                case "DeleteEmployee":
                    endpoint.DeleteEmployee(parameters);
                    break;
                case "ListEmployee":
                    endpoint.ListEmployee(parameters);
                    break;
                case "UpdateEmployee":
                    endpoint.UpdateEmployee(parameters);
                    break;
                case "AddIngredient":
                    endpoint.AddIngredients(parameters);
                    break;
                case "DeleteIngredient":
                    endpoint.DeleteIngredients(parameters);
                    break;
                case "ListIngredient":
                    endpoint.ListIngredients(parameters);
                    break;
                case "UpdateIngredient":
                    endpoint.DeleteIngredients(parameters);
                    break;
                default:
                    throw new Exception("Error occured[warning]");
                    break;

            }
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
    public class EndPoint
    {
        /// <summary>
        /// > Add Article $name $price $type $ingredients
        /// > $returns $id to screen
        /// </summary>
        /// <param name="p"></param>
        public async void AddArticle(List<Parameter> p)
        {
            var article = new Articles();
            article.Name = p.GetAndShift("name").Value;
            if(!p.GetAndShift("baseprice").isReal)
            {
                throw new Exception("Price is not float");
            }
            article.BasePrice = p.GetAndShift("baseprice").Value;
            article.Type = p.GetAndShift("type").Value;
            article.Ingredients = p.GetAndShift("ingredients").Value;
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            int n = (await repo.InsertWithReturnAsync(article));
            View.WriteLine(String.Format($"Added {n} Article"));
        }
        /// <summary>
        /// > Delete Article $id
        /// </summary>
        /// <param name="p"></param>
        public async void DeleteArticle(List<Parameter> p)
        {
            int id;
            if (!p.GetAndShift("id").isInteger)
            {
                throw new Exception("No valid id supplied");
            }
            else
            {
                id = p.GetAndShift("id").Value;
            }
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format("Deleted row {0}", id));
        }
        /// <summary>
        /// > List Articles
        /// </summary>
        /// <param name="p"></param>
        public async void ListArticle(List<Parameter> p)
        {
            List<Articles> articles = new List<Articles>();
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            articles = (await repo.GetAllAsync()).ToList();
            articles[0].PrintKeys();
            articles.ForEach(article => article.Print());
        }
        /// <summary>
        /// > Update Article $todo
        /// </summary>
        /// <param name="p"></param>
        public async void UpdateArticle(List<Parameter> p)
        {
            View.WriteLine("Not yet implemented");
        }
        /// <summary>
        /// > Add Employee $firstname $lastname $email $password
        /// </summary>
        /// <param name="p"></param>
        public async void AddEmployee(List<Parameter> p)
        {
            Employees newEmployee = new Employees();
            newEmployee.Name = p.GetAndShift("firstname").Value;
            newEmployee.LastName = p.GetAndShift("lastname").Value;
            newEmployee.Email = p.GetAndShift("email").Value;
            newEmployee.Password = p.GetAndShift("password").Value;
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            int n = (await repo.InsertWithReturnAsync(newEmployee));
            View.WriteLine(String.Format($"Added {n} employee"));
        }
        /// <summary>
        /// > Delete Employee $id
        /// </summary>
        /// <param name="p"></param>
        public async void DeleteEmployee(List<Parameter> p)
        {
            int id;
            if (!p.GetAndShift("id").isInteger)
            {
                throw new Exception("No valid id supplied");
            }
            else
            {
                id = p.GetAndShift("id").Value;
            }
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format($"Deleted row with id={id}"));
        }
        /// <summary>
        /// List Employees
        /// </summary>
        /// <param name="p"></param>
        public async void ListEmployee(List<Parameter> p)
        {
            List<Employees> employees = new List<Employees>();
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            var res = (await repo.GetAllAsync()); // <== todo; some kind of error here. Cant convert .ToList()
            int c = 0;
            foreach(var item in res)
            {
                if(c == 0)
                {
                    item.PrintKeys();
                }
                item.Print();
                c += 1;
            }
        }
        /// <summary>
        /// > Update Employee
        /// </summary>
        /// <param name="p"></param>
        public async void UpdateEmployee(List<Parameter> p)
        {
            View.WriteLine("Not yet implemented");
        }
        /// <summary>
        /// > Add Ingredient $name $price
        /// </summary>
        /// <param name="p"></param>
        public async void AddIngredients(List<Parameter> p)
        {
            Ingredients ingredients = new Ingredients();
            ingredients.Name = p.GetAndShift("name").Value;
            if (!p.GetAndShift("price").isReal)
            {
                throw new Exception("$price was invalid");
            }
            else
            {
                ingredients.Price = p.GetAndShift("price").Value;
            }
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            int id = (await repo.InsertWithReturnAsync(ingredients));
            View.WriteLine(String.Format($"Added {1} Ingredient"));
        }
        /// <summary>
        /// > Delete Ingredient $id
        /// </summary>
        /// <param name="p"></param>
        public async void DeleteIngredients(List<Parameter> p)
        {
            int id;
            if (!p.GetAndShift("id").isInteger)
            {
                throw new Exception("No valid id supplied");
            }
            else
            {
                id = p.GetAndShift("id").Value;
            }
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format("Deleted row {0}", id));
        }
        /// <summary>
        /// > List Ingrededients
        /// </summary>
        /// <param name="p"></param>
        public async void ListIngredients(List<Parameter> p)
        {
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            var res = (await repo.GetAllAsync());
            int c = 0;
            foreach (var item in res)
            {
                if (c == 0)
                {
                    item.PrintKeys();
                }
                item.Print();
                c += 1;
            }
        }
        /// <summary>
        /// > Update Ingredients
        /// </summary>
        /// <param name="p"></param>
        public async void UpdateIngredients(List<Parameter> p)
        {
            View.WriteLine("Not yet implemented");
        }
        public async void AddOrder(List<Parameter> p)
        {
            View.WriteLine("Not yet implemented");
        }
        public async void DeleteOrder(List<Parameter> p)
        {
            View.WriteLine("Not yet implemented");
        }
        public async void ListOrder(List<Parameter> p)
        {
            var repo = new MsSqlRepo.OrdersRepository("Orders");
            var res = (await repo.GetAllAsync());
            int c = 0;
            foreach (var item in res)
            {
                if (c == 0)
                {
                    item.PrintKeys();
                }
                item.Print();
                c += 1;
            }
        }
        public async void EditOrder(List<Parameter> p)
        {
            View.WriteLine("Not yet implemented");
        }
        /// <summary>
        /// > -help
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
    public static class Extensions
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
        /// <summary>
        /// Same funcionailty as in javascript. Return copy and then removes first element of List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
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
        /// <summary>
        /// For use with object 'Parameter'
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Parameter GetAndShift(this List<Parameter> list, string key)
        {
            bool foundIt = false;
            int i = 0;
            foreach (Parameter p in list)
            {
                if (p.Key == key)
                {
                    foundIt = true;
                    break;
                }
                i += 0;
            }
            if (foundIt)
            {
                var outvar = list[i];
                list.RemoveAt(i);
                return outvar;
            }
            else
            {
                return list.Shift();
            }
        }
        /// <summary>
        /// First char of string into capital letter
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => input.First().ToString().ToUpper() + input.Substring(1)
            };
    }
}
