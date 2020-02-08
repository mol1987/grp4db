﻿using System;
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

            string methodname = this.Command + this.Resource;
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
        public async void AddArticle(List<Match> p)
        {
            var article = new Articles();
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            int id = (await repo.InsertWithReturnAsync(article));
            View.WriteLine(String.Format($"Inserted with id={id}"));
        }
        /// <summary>
        /// > Delete Article $id
        /// </summary>
        /// <param name="p"></param>
        public async void DeleteArticle(List<Match> p)
        {
            int id;
            if (!Int32.TryParse(p[0].Value, out id))
            {
                throw new Exception("Id is invalid");
            };
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format("Deleted row {0}", id));
        }
        /// <summary>
        /// > List Articles
        /// </summary>
        /// <param name="p"></param>
        public async void ListArticle(List<Match> p)
        {
            List<Articles> articles = new List<Articles>();
            var repo = new MsSqlRepo.ArticlesRepository("Articles");
            articles = (await repo.GetAllAsync()).ToList();
            articles[0].PrintKeys();
            articles.ForEach(article => article.PrintRow());
        }
        /// <summary>
        /// > Update Article $todo
        /// </summary>
        /// <param name="p"></param>
        public async void UpdateArticle(List<Match> p) => View.WriteLine("4");
        public async void UpdateIngredients(List<Match> p) => View.WriteLine("8");
        /// <summary>
        /// > Add Employee $firstname $lastname $email $password
        /// </summary>
        /// <param name="p"></param>
        public async void AddEmployee(List<Match> p) => View.WriteLine("13");
        /// <summary>
        /// > Delete Employee $id
        /// </summary>
        /// <param name="p"></param>
        public async void DeleteEmployee(List<Match> p)
        {
            int id;
            if (!Int32.TryParse(p[0].Value, out id))
            {
                throw new Exception("Id is invalid");
            };
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format("Deleted row {0}", id));
        }
        /// <summary>
        /// List Employees
        /// </summary>
        /// <param name="p"></param>
        public async void ListEmployee(List<Match> p)
        {
            var repo = new MsSqlRepo.EmployeesRepository("Employees");
            var res = (await repo.GetAllAsync());
            foreach (var item in res)
            {
                Console.WriteLine($"{item.ID} {item.Name} {item.LastName} {item.Email} {item.Password}");
            }

        }
        /// <summary>
        /// > Update Employee
        /// </summary>
        /// <param name="p"></param>
        public async void UpdateEmployee(List<Match> p) => View.WriteLine("16");
        /// <summary>
        /// > Add Ingredient $name $price
        /// </summary>
        /// <param name="p"></param>
        public async void AddIngredients(List<Match> p){
            Ingredients ingredients = new Ingredients();
            ingredients.Name = p[0].Value;
            ingredients.Price = Int32.Parse(p[1].Value);
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            await repo.DryInsert(ingredients);
            
        }
        /// <summary>
        /// > Delete Ingredient $id
        /// </summary>
        /// <param name="p"></param>
        public async void DeleteIngredients(List<Match> p)
        {
            int id;
            if (!Int32.TryParse(p[0].Value, out id))
            {
                throw new Exception("Id is invalid");
            };
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            await repo.DeleteRowAsync(id);
            View.WriteLine(String.Format("Deleted row {0}", id));
        }
        /// <summary>
        /// > List Ingrededients
        /// </summary>
        /// <param name="p"></param>
        public async void ListIngredients(List<Match> p)
        {
            var repo = new MsSqlRepo.IngredientsRepository("Ingredients");
            var res = (await repo.GetAllAsync());
            foreach (var item in res)
            {
                Console.WriteLine($"{item.ID} {item.Name} {item.Price}");
            }
        }
        //public async void AddOrder(List<Match> p) => View.WriteLine("9");
        //public async void DeleteOrder(List<Match> p) => View.WriteLine("10");
        //public async void ListOrder(List<Match> p) => View.WriteLine("11");
        //public async void EditOrder(List<Match> p) => View.WriteLine("12");
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
