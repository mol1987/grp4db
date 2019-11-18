using System;
using System.Collections.Generic;
using System.Text;

namespace BeställningsTerminal

{
    public class Drink
    {
        public string name { get; set; }
        public double price { get; set; }

        public Drink(string name)
        {
            this.name = name;
            
            if(name == "Läsk")
            {
                price += 20;
            }
            else if (name == "Öl")
            {
                price += 50;
            }
            else if (name == "Vin")
            {
                price += 50;
            }

        }
    }
}
