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
            new RegexCommand("command", new Regex("(?<command>)^[Aa]dd|^[Dd]elete|^[Uu]pdate|^[Ll]ist")),
            new RegexCommand("resource", new Regex("(?<resource>)[Aa]rticles?|[Ee]mployees?|[Ii]ngredients?")),
            new RegexCommand("flags", new Regex("(?<flag>-?-\\w+)")),
            new RegexCommand("helpflag", new Regex("-help|--help|-h"))
        };
        public static Dictionary<string, Flag> Flags = new Dictionary<string, Flag>()
        {
            {"help", new Flag{Key="help", Varations = new List<string>(){"-help","--help","-h"}, Callback = new EndPoint().HelpFlag }}
        };
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
    }
}
