using System;
using System.Collections.Generic;
using System.Text;

namespace CostumerTerminalFinished
{
    public class Salad
    {
        public string name { get; set; }
        public double price { get; set; }

        public Salad(string name)
        {
            this.name = name;
            price += 80;
        }
    }
}
