using System;
using System.Collections.Generic;
using System.Linq;
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

        public Operation(string input)
        {
            Input = input;
            OriginalInput = input; // Kept for future comparisons/checks

            Input = Input.TrimStart();
            List<string> parameters = new List<string>();
            List<Match> quotatedParameters = new List<Match>();
            List<Match> flags = Constant.GetRegexByKey("flags").Matches(Input).ToList();
            Command command = new Command(Constant.GetRegexByKey("command").Match(Input));
            Resource resource = new Resource(Constant.GetRegexByKey("resource").Match(Input));

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
            int n = (command.Match.Value.Length + resource.Match.Value.Length) + 1;
            Input = Input.Remove(command.Match.Index, command.Match.Value.Length).Remove(resource.Match.Index - (command.Match.Value.Length + 1), resource.Match.Value.Length);
            flags.ForEach(flag => Input = Input.Remove(flag.Index - n, flag.Length));

            // Values inside ""-quotes are to be kept together
            if (Input.Contains('"') || Input.Contains('\'') || Input.Contains('`'))
            {

            }

            // Parse the rest into parameters
            Input.Split(' ').ToList().ForEach(a => parameters.Add(a));
            
            // If all is well, execute dynamically
            Action action = new Action();
            action.Parameters = parameters;
            string methodname = command.Value + resource.Value;
            action.GetType().GetMethod(methodname).Invoke(action, new[] { "Hello" });
            #region
            //flags = new Flags(parts);

            //if (flags.HasHelpFlag())
            //{
            //    View.WriteLine("todo; error flag activated");
            //    return;
            //}
            //if (parts.Count < 2)
            //{
            //    View.WriteLine("todo; input error");
            //    return;
            //}
            //if (!parts.ContainsItemFrom(AllowedActions["Command"]))
            //{
            //    View.WriteLine("todo; Invalid Command");
            //    return;
            //}
            //if (!parts.ContainsItemFrom(AllowedActions["Resource"]))
            //{
            //    View.WriteLine("todo; Invalid Resource");
            //    return;
            //}

            //command = new Command(parts[0]);
            //resource = new Resource(parts[1]);

            //if (parts.Count >= 3 && flags.HasFlags())
            //{
            //    parameters = new Parameters(parts);
            //}

            //parameters = new Parameters();
            #endregion
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
    }
    public class Action
    {
        public List<string> Parameters { get; set; }
        public void AddArticle(string s)
        {
            //var missing = Parameters.Missing(new List<string>() { "Name", "BasePrice", "Type" });
            // Desc is frivillig
            if(new List<string>() { "Name", "BasePrice", "Type" }.Count - Parameters.Count !> 0)
            {
                throw new Exception($"Missing {Parameters.Count} arguments for 'Add Article'");
            }
            Articles newArticle = new Articles();
            newArticle.Name = this.Parameters[0];
            newArticle.BasePrice = (float)Convert.ToDouble(this.Parameters[1]);
            newArticle.Type = this.Parameters[2];
            newArticle.PrintKeys();
            newArticle.PrintRow();
        }
        public void DeleteArticle(string s) => View.WriteLine("2");
        public void ListArticle(string s) => View.WriteLine("3");
        public void EditArticle(string s) => View.WriteLine("4");
        public void AddItem(string s) => View.WriteLine("5");
        public void DeleteItem(string s) => View.WriteLine("6");
        public void ListItem(string s) => View.WriteLine("7");
        public void EditItem(string s) => View.WriteLine("8");
        public void AddOrder(string s) => View.WriteLine("9");
        public void DeleteOrder(string s) => View.WriteLine("10");
        public void ListOrder(string s) => View.WriteLine("11");
        public void EditOrder(string s) => View.WriteLine("12");
        public void AddEmployee(string s) => View.WriteLine("13");
        public void DeleteEmployee(string s) => View.WriteLine("14");
        public void ListEmployee(string s) => View.WriteLine("15");
        public void EditEmployee(string s) => View.WriteLine("16");

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
    public class Command
    {
        public string Value { get; set; }
        public bool Success { get; set; }
        public Match Match { get; set; }
        public Command(Match value)
        {
            Value = value.Value.FirstCharToUpper();
            Success = value.Success;
            this.Match = value;
        }
    }
    public class Resource
    {
        public string Value { get; set; }
        public bool Success { get; set; }
        public Match Match { get; set; }
        public Resource(Match value)
        {
            Value = value.Value.FirstCharToUpper();
            Success = value.Success;
            this.Match = value;
        }
    }
    //public class Command
    //{
    //    public string Value { get; set; }

    //    private Dictionary<int, Delegate> Callbacks = new Dictionary<int, Delegate>()
    //    {
    //        {0, new Func<int>(Hello)}
    //    };
    //    public Command(string str)
    //    {
    //        Value = str.FirstCharToUpper();
    //    }
    //    private static int Hello()
    //    {
    //        Console.Write("hello\n");
    //        return 1;
    //    }
    //}
    //public class Resource
    //{
    //    public string Value { get; set; }
    //    public Resource(string str)
    //    {
    //        Value = str.FirstCharToUpper();
    //    }
    //}
    //public class Parameters
    //{
    //    public List<string> parameters;
    //    public Parameters()
    //    {
    //        parameters = new List<string>() { };
    //    }
    //    public Parameters(List<string> parameters)
    //    {
    //        parameters = new List<string>() { };
    //        parameters.Shift();
    //        parameters.Shift();
    //        parameters.AddRange(parameters.Where(param => param.Substring(0) != "-").ToList());
    //    }
    //}
    //public class Flags
    //{
    //    private List<string> flags;
    //    public Flags()
    //    {
    //        flags = new List<string>() { };
    //    }
    //    public Flags(List<string> parts)
    //    {
    //        //filter for --flags
    //        flags = new List<string>() { };
    //        flags = parts.Where(part => part.Substring(0) == "-").ToList();
    //    }
    //    public bool HasFlags()
    //    {
    //        return flags.Count < 0;
    //    }
    //    public bool HasHelpFlag()
    //    {
    //        return !HasFlags() ? false : flags.Contains("--help") || flags.Contains("--h") ? true : false;
    //    }
    //}
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
        public static string FirstCharToUpper(this string str)
        {
            return (str.First().ToString().ToUpper() + str.Substring(1,str.Length-1));
        }
    }
}
