using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;

namespace AdminTerminal
{
    public static class Global
    {
        public static bool IsRunning;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Global.IsRunning = true; // Is reachable from everywhere, setting false quit the programs immediatly
            //
            while (Global.IsRunning)
            {
                Operation op = new Operation(ParseLine(Console.ReadLine()));
            }
        }

        static List<string> ParseLine(string line)
        {
            var AllowedPattern = new Regex("^[a-zA-Z0-9-]+$");
            List<string> lines = line.Split(' ').ToList();
            lines = lines.Where(a => a != " " && a != "").ToList();
            bool isValid = lines.All(a => AllowedPattern.IsMatch(a));// todo; move this elsewhere
            return lines;
        }
    }

    public class Operation
    {
        private Dictionary<string, List<string>> AllowedActions = new Dictionary<string, List<string>>()
        {
            {"Command", new List<string>(){"Add", "Delete", "List", "Edit"}},
            {"Resource", new List<string>(){"Article", "Item", "Order", "Employee"}},
            {"Parameters", new List<string>(){}},
            {"Flags", new List<string>(){"--help"}},
        };
        //Dictionary<string, Dictionary<string, Func<string>>> callback = new Dictionary<string, Dictionary<string, Func<string>>>()
        //{
        //    {"Add",new Dictionary<string, Func<string>>()
        //    {
        //        {"Article", new Func<string>(AddArticle)}
        //    }}
        //};
        private Command command;
        private Resource resource;
        private Parameters parameters;
        private Flags flags;
        public Operation(List<string> parts)
        {
            flags = new Flags(parts);

            if (flags.HasHelpFlag())
            {
                View.WriteLine("todo; error flag activated");
                return;
            }
            if (parts.Count < 2)
            {
                View.WriteLine("todo; input error");
                return;
            }
            if (!parts.ContainsItemFrom(AllowedActions["Command"]))
            {
                View.WriteLine("todo; Invalid Command");
                return;
            }
            if (!parts.ContainsItemFrom(AllowedActions["Resource"]))
            {
                View.WriteLine("todo; Invalid Resource");
                return;
            }

            command = new Command(parts[0]);
            resource = new Resource(parts[1]);

            if (parts.Count >= 3 && flags.HasFlags())
            {
                parameters = new Parameters(parts);
            }

            parameters = new Parameters();


            // If all is well, execute dynamically
            try
            {
                Action action = new Action();
                action.parameters = parameters;
                string methodname = command.Value + resource.Value;
                action.GetType().GetMethod(methodname).Invoke(action, new[] { "Hello" });
            }
            catch (TargetParameterCountException e)
            {
                View.WriteLine("todo; Error parameter count mismatch");
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
    }
    public class Action
    {
        public Parameters parameters { get; set; }
        public void AddArticle(string s) => View.WriteLine("1");
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
    }
    public class Command
    {
        public string Value { get; set; }

        private Dictionary<int, Delegate> Callbacks = new Dictionary<int, Delegate>()
        {
            {0, new Func<int>(Hello)}
        };
        public Command(string str)
        {
            Value = str.FirstCharToUpper();
        }
        private static int Hello()
        {
            Console.Write("hello\n");
            return 1;
        }
    }
    public class Resource
    {
        public string Value { get; set; }
        public Resource(string str)
        {
            Value = str.FirstCharToUpper();
        }
    }
    public class Parameters
    {
        public List<string> parameters;
        public Parameters()
        {
            parameters = new List<string>() { };
        }
        public Parameters(List<string> parameters)
        {
            parameters = new List<string>() { };
            parameters.Shift();
            parameters.Shift();
            parameters.AddRange(parameters.Where(param => param.Substring(0) != "-").ToList());
        }
    }
    public class Flags
    {
        private List<string> flags;
        public Flags()
        {
            flags = new List<string>() { };
        }
        public Flags(List<string> parts)
        {
            //filter for --flags
            flags = new List<string>() { };
            flags = parts.Where(part => part.Substring(0) == "-").ToList();
        }
        public bool HasFlags()
        {
            return flags.Count < 0;
        }
        public bool HasHelpFlag()
        {
            return !HasFlags() ? false : flags.Contains("--help") || flags.Contains("--h") ? true : false;
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
        public static string FirstCharToUpper(this string str)
        {
            return str.First().ToString() + str.Substring(1);
        }
    }
}
