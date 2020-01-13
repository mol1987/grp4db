using System;
using System.Collections.Generic;
using System.Text;

namespace BeställningsTerminal
{
    public class Pasta
    {
        public string name { get; set; }
        public double price { get; set; }

        public Pasta(string name)
        {
            this.name = name;
            price = 80;
        }
    }
}
