using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdminTerminal
{
    public static class Constant
    {
        public static string HelpString = String.Format("{0}\n\t{1}\n\t{0}", "todo; something here something here..", "\n", "Method here method here");
        public static List<RegexCommand> Regex = new List<RegexCommand>()
        {
            new RegexCommand("command", @"(?<command>)^[Aa]dd|^[Dd]elete|^[Uu]pdate|^[Ll]ist"),
            new RegexCommand("resource", @"(?<resource>)[Aa]rticle|[Ee]mployee|[Ii]ngredients?"),
            new RegexCommand("flags", @"(?<flag>-?-\w+)"),
            new RegexCommand("quotationParam", new Regex(@"(['""`][a-zA-Z0-9]+['""`])")),
            //new RegexCommand("AddArticle", @"(?<name>)")
        };
        public static Dictionary<string, Flag> Flags = new Dictionary<string, Flag>()
        {
            {"help", new Flag{Key="help", Varations = new List<string>(){"-help","--help","-h"}, Callback = new EndPoint().HelpFlag }}
            //new Flag{Key="help", Varations = new List<string>(){"-help","--help","-h"}, Callback = new Action().HelpFlag }
            // -p ?
        };
        //public static List<Parameters> ParameterList = new List<Parameters>()
        //{
        //    new Parameters("AddArticle", new[]{ "Name","Desc","Type","Price"}, new dynamic[]{"string","string","string",0.1f}, new bool[]{true,false,true,true}),
        //    new Parameters("DeleteArticle", new[]{ "ID"}, new dynamic[]{1}, new bool[]{true}),
        //    new Parameters("UpdateArticle", new[]{ "ID","Name","Desc","Type","Price"}, new dynamic[]{1,"string","string","string",0.1f}, new bool[]{true,true,false,true,true}),
        //    new Parameters("ListArticle", new[]{ "ID", "Name"}, new dynamic[]{true,true}, new bool[]{false,false}),

        //    new Parameters("AddEmployee", new[]{ "Name","LastName","Email","Password"}, new dynamic[]{"string","string","string","string"}, new bool[]{true,true,false,false}),
        //    new Parameters("DeleteEmployee", new[]{"ID"}, new dynamic[]{1}, new bool[]{true}),
        //    new Parameters("UpdateEmployee", new[]{ "ID","Name","LastName","Email","Password"}, new dynamic[]{"string","string","string",0.1f}, new bool[]{true,true,true, false, false}),
        //    new Parameters("ListEmployee", new[]{ "ID", "Name"}, new dynamic[]{true,true}, new bool[]{false,false}),
        //};

        //public class Parameters
        //{
        //    public string Key { get; set; }
        //    public List<string> Values
        //    {
        //        get; set;
        //        //get
        //        //{
        //        //    return Values;
        //        //}
        //        //set
        //        //{
        //        //    value.ForEach(val => Values.Add(val));
        //        //}
        //    }
        //    public List<Type> Types { get; set; }
        //    public List<bool> Required { get; set; }
        //    public Parameters(string key, string[] values, dynamic[] types, bool[] required)
        //    {
        //        Key = key;
        //        Values = values.ToList();
        //        types.ToList().ForEach(_ => Types.Add(_.GetType()));
        //        Required = required.ToList();
        //    }
        //    //public List<Parameter> Parameter { get; set; }
        //}
        //public class Parameter
        //{
        //    public string Value { get; set; }
        //    public dynamic Type { get; set; }
        //    public bool Required { get; set; }
        //}
        public class Flag
        {
            public string Key { get; set; }
            public List<string> Varations { get; set; }
            public Func<string, string, string> Callback { get; set; }
        }
        public class RegexCommand
        {
            public string Command { get; set; }
            public Regex Value { get; set; }
            public RegexCommand(string Command, string Value)
            {
                this.Command = Command;
                this.Value = new Regex(Value);
            }
            public RegexCommand(string Command, Regex Value)
            {
                this.Command = Command;
                this.Value = Value;
            }
        }
        /// <summary>
        /// Return regex variable
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Regex GetRegexByKey(string key)
        {
            return Regex.Find(a => a.Command == key).Value;
        }
        public static bool HasHelpFlag(List<Match> flags)
        {
            bool hasFlag = false;
            flags.ForEach(flag =>
            {
                if (Constant.Flags["help"].Varations.Contains(flag.Value))
                {
                    hasFlag = true;
                }
            });
            return hasFlag;
        }
    }
}
