using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private List<Parameter> Parameters = new List<Parameter>();

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
            MatchCollection mc = a.Matches(Input);
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
                Parameters.Add(new Parameter(group, m.Groups[group].Value));
            }

            // Joined for semantical pathing
            string methodname = this.Command + this.Resource;

            /******
            *
            ******/
            var newArticle = new Articles();
            var newEmployee = new Employees();
            var newIngredients = new Ingredients();
            try
            {
                switch (methodname)
                {
                    case "AddArticle":
                        AddArticle().Wait();
                        break;
                    case "DeleteArticle":
                        DeleteArticle().Wait();
                        break;
                    case "ListArticle":
                        ListArticle().Wait();
                        break;
                    case "UpdateArticle":
                        UpdateArticle().Wait();
                        break;
                    case "AddEmployee":
                        AddEmployee().Wait();
                        break;
                    case "DeleteEmployee":
                        DeleteEmployee().Wait();
                        break;
                    case "ListEmployee":
                        ListEmployee().Wait();
                        break;
                    case "UpdateEmployee":
                        UpdateEmployee().Wait();
                        break;
                    case "AddIngredient":
                        AddIngredient().Wait();
                        break;
                    case "DeleteIngredient":
                        DeleteIngredient().Wait();
                        break;
                    case "ListIngredient":
                        ListIngredient().Wait();
                        break;
                    case "UpdateIngredient":
                        UpdateIngredient().Wait();
                        break;
                    default:
                        throw new Exception("Error occured[warning]");
                        break;

                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private async Task AddArticle()
        {
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            var newArticle = new Articles();
            newArticle.Name = Parameters.GetAndShift("name").Value;
            newArticle.BasePrice = Parameters.GetAndShift("price").Value;
            newArticle.Type = Parameters.GetAndShift("type").Value;
            int n = (await repo.InsertWithReturnAsync(newArticle));
            View.WriteLine(String.Format($"Added {n} Article"));
        }
        private async Task DeleteArticle()
        {
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            int id = Parameters.GetAndShift("id").Value;
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format("Deleted row {0}", id));
        }
        private async Task ListArticle()
        {
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            List<Articles> articles = new List<Articles>();
            articles = (await repo.GetAllAsync()).ToList();
            articles[0].PrintKeys();
            articles.ForEach(article => article.Print());
        }
        private async Task UpdateArticle()
        {
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            var articleToUpdate = new Articles();
            articleToUpdate.ID = Parameters.GetAndShift("id").Value;
            foreach(var item in Parameters)
            {
                if (item.Key == "name")
                {
                    articleToUpdate.Name = item.Value;
                }
                if (item.Key == "price")
                {
                    articleToUpdate.BasePrice = item.Value;
                }
                if (item.Key == "type")
                {
                    articleToUpdate.Type = item.Value;
                }
            }
            var articleList = new List<Articles>() { articleToUpdate };
            await repo.UpdateAsync(articleToUpdate);
            View.WriteLine("1 article was updated");
        }
        private async Task AddEmployee()
        {
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            var newEmployee = new Employees();

            newEmployee.Name = Parameters.GetAndShift("firstname").Value;
            newEmployee.LastName = Parameters.GetAndShift("lastname").Value;
            newEmployee.Email = Parameters.GetAndShift("email").Value;
            newEmployee.Password = Parameters.GetAndShift("password").Value;
            int n = (await repo.InsertWithReturnAsync(newEmployee));
            View.WriteLine(String.Format($"Added {n} employee"));
        }
        private async Task DeleteEmployee()
        {
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            int id;
            id = Parameters.GetAndShift("id").Value;
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format($"Deleted row with id={id}"));
        }
        private async Task ListEmployee()
        {
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            List<Employees> employees = new List<Employees>();
            employees = (await repo.GetAllAsync()).ToList();
            employees[0].PrintKeys();
            employees.ForEach(article => article.Print());
        }
        private async Task UpdateEmployee()
        {
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            var employeeToUpdate = new Employees();
            employeeToUpdate.ID = Parameters.GetAndShift("id").Value;
            foreach (var item in Parameters)
            {
                if (item.Key == "name")
                {
                    employeeToUpdate.Name = item.Value;
                }
                if (item.Key == "lastname")
                {
                    employeeToUpdate.LastName = item.Value;
                }
                if (item.Key == "email")
                {
                    employeeToUpdate.Email = item.Value;
                }
                if (item.Key == "password")
                {
                    employeeToUpdate.Password = item.Value;
                }
            }
            await repo.UpdateAsync(employeeToUpdate);
            View.WriteLine("1 employee was updated");
        }
        private async Task AddIngredient()
        {
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            Ingredients ingredients = new Ingredients();
            ingredients.Name = Parameters.GetAndShift("name").Value;
            ingredients.Price = Parameters.GetAndShift("price").Value;
            int id = (await repo.InsertWithReturnAsync(ingredients));
            View.WriteLine(String.Format($"Added {1} Ingredient"));
        }
        private async Task DeleteIngredient()
        {
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            int id = Parameters.GetAndShift("id").Value;
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format("Deleted row {0}", id));
        }
        private async Task ListIngredient()
        {
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            List<Ingredients> ingredients = new List<Ingredients>();
            ingredients = (await repo.GetAllAsync()).ToList();
            ingredients[0].PrintKeys();
            ingredients.ForEach(a => a.Print());
        }
        private async Task UpdateIngredient()
        {
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            var ingredientToUpdate = new Ingredients();
            ingredientToUpdate.ID = Parameters.GetAndShift("id").Value;
            foreach (var item in Parameters)
            {
                if (item.Key == "name")
                {
                    ingredientToUpdate.Name = item.Value;
                }
                if (item.Key == "price")
                {
                    ingredientToUpdate.Price = item.Value;
                }
            }
            await repo.UpdateAsync(ingredientToUpdate);
            View.WriteLine("1 ingredient was updated");
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
        public static Parameter GetAndShift(this List<Parameter> list, string key, dynamic defaultvalue)
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
                list.Shift();
                return defaultvalue;
            }
        }
        public static Boolean ParameterHasKey(this List<Parameter> list, string key)
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
            return foundIt;
        }

        //public static List<Ingredients> IngredientStringToIngredientList(this String str)
        //{
        //    List<Ingredients> newIngredients = new List<Ingredients>();
        //    foreach(var item in str.Split(',').ToList())
        //    {
        //        Ingredients ingredients = new Ingredients();
        //        ingredients.Name = 
        //    }

        //}
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
