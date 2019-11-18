using System;
using System.Collections.Generic;
using System.Text;

namespace BeställningsTerminal
{
    public class Pizza
    {
        public List<string> IngredientList = new List<string>();
        public List<string> ExtraIngredientList = new List<string>();

        public string name { get; set; }

        public string bottom { get; set; }

        public double price { get; set; }


        public Pizza(string name)
        {
            this.name = name;
        }

        public string AddBottom(string bottom)
        {
            this.bottom = bottom;
            price += 80;

            return bottom;
        }

        public void AddIngredient(string ingredient)
        {
            IngredientList.Add(ingredient);
        }

        public void AddExtraIngredient(string ingredient)
        {
            ExtraIngredientList.Add(ingredient);
            price += 5;
        }

        public int CountIngredients()
        {
            int counter = IngredientList.Count + ExtraIngredientList.Count;

            return counter;
        }

        public void RemoveIngredient(int index)
        {
            IngredientList.RemoveAt(index-1);
        }

        public void RemoveExtraIngredient(int index)
        {
            ExtraIngredientList.RemoveAt(index-1);
            price -= 5;
        }


        public string ShowIngredients()
        {
            string ingredients = "";
            
            foreach (string ingredient in IngredientList)
            {
               
                if (ingredient == IngredientList[IngredientList.Count - 1])
                {
                    ingredients += $"{ingredient}";
                }

                else
                {
                    ingredients += $"{ingredient} ";
                }
            }

            
            return ingredients;
        }

        public string ShowExtraIngredients()
        {
            string extraIngredients = "";

            foreach (string extras in ExtraIngredientList)
            {
                if (extras == ExtraIngredientList[ExtraIngredientList.Count - 1])
                {
                    extraIngredients += $"+{extras} ";
                }
               
                else
                {
                    extraIngredients += $"+{extras} ";
                }
            }

            return extraIngredients;
        }
    }
}
