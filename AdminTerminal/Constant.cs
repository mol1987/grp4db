using System;
using System.Collections.Generic;
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
        };
        public static Dictionary<string, Flag> Flags = new Dictionary<string, Flag>()
        {
            {"help", new Flag{Key="help", Varations = new List<string>(){"-help","--help","-h"}, Callback = new Action().HelpFlag }}
            //new Flag{Key="help", Varations = new List<string>(){"-help","--help","-h"}, Callback = new Action().HelpFlag }
            // -p ?
        };
        public class Command { }
        public class Resource { }
        public class Parameter { }
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
            flags.ForEach(flag => {
                if (Constant.Flags["help"].Varations.Contains(flag.Value))
                {
                    hasFlag = true;
                }
            });
            return hasFlag;
        }
    }
}
