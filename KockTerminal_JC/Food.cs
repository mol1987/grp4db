using System;
using System.Collections.Generic;
using System.Text;


namespace KockTerminal_JC
{
    public class Food
    {
        public string name { get; set; }
        public static List<string> foods = new List<string>
        {  "Vesuvio", "Margarita", "Hawaii", "Calzone",
            "Cannelloni med kött", "Cannelloni vegetarisk",
            "Mozzarellasallad", "Chevresallad"
        };
        public static List<string> ingredients = new List<string>
        {
            "Tomatsås", "Ost", "Skinka", "Ananas", "Cannelloni", "Köttfärs", "Spenat",
            "Sallad", "Tomat", "Mozzarella", "Chevre"
        };
        public string GenerateFood()
        {
            Random randFood = new Random();
            int randFoodIndex = randFood.Next(1, 9);
            name = foods[randFoodIndex - 1];
            return name;
        }
        public void ShowOrder(string name,List<Food>orders,int listIndex)
        {
            bool correctKey = false;
            while(correctKey== false)
            {
                Console.Clear();
                Console.WriteLine(" du har valt" + name);
                Console.WriteLine();
                Console.WriteLine("denna  rätt innehåller följande ingridenter");
                switch (name)
                {
                    case "Vesuvio":
                        Console.WriteLine(ingredients[0]); //tomatsås
                        Console.WriteLine(ingredients[1]); // ost
                        Console.WriteLine(ingredients[2]); // skinka
                        Console.WriteLine("\n-------------");
                        Console.WriteLine("1. Stoppa in i ugn");
                        break;
                    case "Margarita":
                        Console.WriteLine(ingredients[0]); // tomatsås
                        Console.WriteLine(ingredients[1]); // ost
                        Console.WriteLine("\n-------------");
                        Console.WriteLine("1. Stoppa in i ugn");
                        break;
                    case "Hawaii":
                        Console.WriteLine(ingredients[0]); // tomatsås
                        Console.WriteLine(ingredients[1]); // ost
                        Console.WriteLine(ingredients[2]); // skinka
                        Console.WriteLine(ingredients[3]); // ananas
                        Console.WriteLine("\n-------------");
                        Console.WriteLine("1. Stoppa in i ugn");
                        break;
                    case "Calzone":
                        Console.WriteLine(ingredients[0]); //tomatsås
                        Console.WriteLine(ingredients[1]); // ost
                        Console.WriteLine(ingredients[2]); // skinka
                        Console.WriteLine("\n-------------");
                        Console.WriteLine("1. Stoppa in i ugn");
                        break;
                    case "Cannelloni med kött":

                        Console.WriteLine(ingredients[4]); // cannelloni
                        Console.WriteLine(ingredients[5]); // köttfärs
                        Console.WriteLine(ingredients[1]); // ost
                        Console.WriteLine("\n-------------");
                        Console.WriteLine("1. Tillaga");
                        break;
                    case "Cannelloni vegetarisk":
                        Console.WriteLine(ingredients[4]); // cannelloni
                        Console.WriteLine(ingredients[6]); // spenat
                        Console.WriteLine(ingredients[1]); // ost
                        Console.WriteLine("\n-------------");
                        Console.WriteLine("1. Tillaga");
                        break;
                    case "Mozzarellasallad":
                        Console.WriteLine(ingredients[7]); // sallad
                        Console.WriteLine(ingredients[8]); // tomat
                        Console.WriteLine(ingredients[9]); // mozzarella
                        Console.WriteLine("\n-------------");
                        Console.WriteLine("1. Tillaga");
                        break;
                    case "Chevresallad":
                        Console.WriteLine(ingredients[7]); // sallad
                        Console.WriteLine(ingredients[8]); // tomat
                        Console.WriteLine(ingredients[10]); // chevre
                        Console.WriteLine("\n-------------");
                        Console.WriteLine("1. Tillaga");
                        break;



                }
                // Alternativ som låter kocken gå tillbaka
                Console.WriteLine("2. Återgå");
                //skapar en int utav användarens val
                int userInput = Console.ReadKey(true).KeyChar - '0';
                /// Check it out again
                 // om det är en pizza som skall tillagas så skickas man till en meny för ugn
                if (userInput == 1 && name == "Vesuvio" || userInput == 1 && name == "Margarita" ||
                    userInput == 1 && name == "Hawaii" || userInput == 1 && name == "Calzone")
                {
                    // om kocken bekräftar tillagning så tas ordern bort från orderlistan
                    orders.RemoveAt(listIndex);
                    CookPizza(orders);
                    correctKey = true;

                }

                // om det är en pasta eller sallad som ska tillagas så skickas man till en annan skärm där man
                // kan bekräfta när man är färdig och maten är klar att serveras
                else if (userInput == 1 && name == "Cannelloni med kött" || userInput == 1 && name == "Cannelloni vegetarisk" ||
                        userInput == 1 && name == "Mozzarellasallad" || userInput == 1 && name == "Chevresallad")
                {
                    // om kocken bekräftar tillagning så tas ordern bort från orderlistan
                    orders.RemoveAt(listIndex);
                    CookSaladOrPasta(orders, name);
                    correctKey = true;
                }

                // åtegår till startskärmen och ritar ut de ordrar som finns pågående
                else if (userInput == 2)
                {
                    Program.GetOrders(orders);
                    correctKey = true;
                }
            }

        }

        // om det är en pizza som ska tillagas (i ugn) så körs denna funktion
        public void CookPizza(List<Food> orders)
        {
            // pizzan ligger i ugnen
            Console.Clear();
            Console.WriteLine("Pizzan tillagas");
            Console.WriteLine("-------------\n");

            // simulering av att pizzan tillagas
            System.Threading.Thread.Sleep(1500);

            // pizzan klar
            Console.Clear();
            Console.WriteLine("Pizzan färdig");
            Console.WriteLine("-------------\n");
            Console.Beep(500, 2000);

            Console.WriteLine("Klicka Enter för att skicka pizzan till servering");

            // väntar tills kocken bekräftat att maten är klar för servering
            while (true)
            {
                char key = Console.ReadKey(true).KeyChar;
                // om kocken klickar på enter så skickas hen tillbaka till startsidan för att kunna ta nya ordrar
                if (key == 13)
                {
                    DrawConfirmationScreen(orders);
                    break;
                }
            }
        }

        // om det är en sallad eller pasta körs denna funktion som är utan ugn-delen
        public void CookSaladOrPasta(List<Food> orders, string name)
        {
            Console.Clear();
            Console.WriteLine(name + " tillagas");
            Console.WriteLine("-------------\n");

            Console.WriteLine("Klicka Enter när du är färdig och redo att skicka maten till servering");

            // väntar tills kocken bekräftat att maten är klar för servering
            while (true)
            {
                char key = Console.ReadKey(true).KeyChar;
                // om kocken klickar på enter så skickas hen tillbaka till startsidan för att kunna ta nya ordrar
                if (key == 13)
                {
                    DrawConfirmationScreen(orders);
                    break;
                }
            }
        }

        public void DrawConfirmationScreen(List<Food> orders)
        {
            Console.Clear();
            Console.WriteLine("Maten serverad!");
            System.Threading.Thread.Sleep(1500);
            Program.GetOrders(orders);
        }
    }
}
            

        
    

