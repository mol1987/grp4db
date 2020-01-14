using System;
using System.Collections.Generic;
using System.Text;

//kommentar

namespace BeställningsTerminal
{
    
    public class Drink
    {
        public string name { get; set; }
        public double price { get; set; }

        public Drink(DrinksList ChoosenDrink)
        {
            this.name = name;

            switch (ChoosenDrink)
            {
                case DrinksList.Läsk:
                    price += 20;
                    break;
                case DrinksList.Öl:
                    price += 50;
                    break;
                case DrinksList.Vin:
                    price += 50;
                    break;
                default:
                    break;
            }
        }
    }
}
