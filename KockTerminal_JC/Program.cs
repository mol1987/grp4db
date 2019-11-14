using System;
using System.Collections.Generic;

namespace KockTerminal_JC
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Food> orders = new List<Food>();

            GetOrders(orders);
        }

        // denna funktion används för att rita ut en startskärm
        // funktionen där in en lista med de ordrar som inkommit så att ordrarna ständigt finns sparade
        public static void GetOrders(List<Food> orders)
        {
            int orderCounter, userInput;
            bool clicked = false;

            Console.Clear();

            while (clicked == false)
            {
                Console.WriteLine("Välj den order som du vill tillaga");
                Console.WriteLine("-------------\n");

                orderCounter = 0;
                
                for (int i = 0; i < 5; i++)
                {
                    orderCounter++;
                    orders.Add(new Food());
                    orders[orders.Count - 1].GenerateFood();
                    Console.WriteLine(orderCounter + ". " + orders[orderCounter - 1].name);
                    System.Threading.Thread.Sleep(500);
                }

                userInput = Console.ReadKey(true).KeyChar - '0';
                    if (userInput > 0 && userInput < 6)
                    {
                        orders[userInput - 1].ShowOrder(orders[userInput - 1].name, orders, userInput - 1);
                        clicked = true;
                    }

                Console.Clear();
            }
        }

    }
}
