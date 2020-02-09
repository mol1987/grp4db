using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdminTerminal
{
    public class Parameter
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public dynamic Value { get; set; }
        public readonly bool isInteger;
        public readonly bool isReal;
        public Parameter(string name, string value)
        {
            Name = name;
            if (Name == "literal")
            {
                value = value.Replace("\"", "").Replace("\\", "");
                Value = value;
            }
            if (Name == "named")
            {
                var splitstr = value.Split('=');
                Key = splitstr.First();
                Value = splitstr.Last();
            }
            else
            {
                Key = "";
                Value = value;
            }
            isReal = new Regex("[-+]?([0-9]*\\.[0-9]+|[0-9]+)").Match(Value).Success;
            isInteger = isReal ? false : new Regex("^\\d+$").Match(Value).Success;

            if (isReal)
            {
                float floatres;
                bool isFloat = float.TryParse(Value, out floatres);
                if (!isFloat)
                {
                    throw new Exception("Invalid float format");
                }
            }
            if (isInteger)
            {
                int intres;
                bool isInt = Int32.TryParse(Value, out intres);
                if (!isInt)
                {
                    throw new Exception("Invalid integer format, can only be [0-9]+");
                }
            }
        }
        public void Print()
        {
            Console.Write("name:{0}, value:{1} key:{2}\n", Name, Value, Key);
        }
    }
}
