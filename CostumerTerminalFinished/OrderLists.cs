using System;
using System.Collections.Generic;
using System.Text;

namespace BeställningsTerminal
{
    public class OrderLists
    {
        public string name { get; set; }

        public List<Pizza> pizzas = new List<Pizza>();
        public List<Pasta> pastas = new List<Pasta>();
        public List<Salad> salads = new List<Salad>();
        public List<Drink> drinks = new List<Drink>();

        public List<object> CurrentOrder = new List<object>();

        public List<string> Ingredients = new List<string>()
        {
            "Tomatsås", "Ost", "Skinka", "Salami", "Lök", "Mozarella", "Ädelost", "Oliver", "Ananas", "Curry"
        };

    }
}
