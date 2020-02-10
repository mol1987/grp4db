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
            int i;
            float f;
            if(Int32.TryParse(Value.ToString(), out i))
            {
                isInteger = true;
                Value = i;
            }
            else if (float.TryParse(Value.ToString(), out f))
            {
                isReal = true;
                Value = f;
            }
            //isInteger = new Regex("^[-+]?([0-9]*\\.[0-9]+|[0-9]+)$").Match(Value).Success;
            //isInteger = new Regex("^\\d+$").Match(Value).Success;
            //int intres;
            //float floatres;
            //if (isReal)
            //{
            //    isInteger = float.TryParse(Value, out floatres);
            //    Value = floatres;
            //}
            //else if (isInteger)
            //{
            //    isReal = Int32.TryParse(Value, out intres);
            //    Value = intres;
            //}

        }
        public void Print()
        {
            Console.Write("name:{0}, value:{1} key:{2}\n", Name, Value, Key);
        }
    }
}
