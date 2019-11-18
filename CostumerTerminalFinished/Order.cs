using System;
using System.Collections.Generic;
using System.Text;

namespace BeställningsTerminal
{
    class Order : OrderLists
    {

        public bool correctKey { get; set; }
        char key;
        int index;
        double totalPrice;
        string italiensk = "Italiensk botten";
        string amerikansk = "Amerikansk botten";

        // Ritar ut en välkomstskärm
        public void StartOrder()
        {

            // För varje gång som en ny order startas eller som programmet avslutas så nollställs alla orderparametrar
            CurrentOrder.Clear();
            pizzas.Clear();
            salads.Clear();
            pastas.Clear();
            drinks.Clear();
            totalPrice = 0;


            while (correctKey == false)
            {
                Console.Clear();
                Console.WriteLine("Hej och välkommen till Pizza palatset. \nKlicka på Enter för att påbörja din beställning.");
                key = Console.ReadKey(true).KeyChar;
                if (key == 13)
                {
                    DrawFoodMenu();
                    correctKey = true;
                }
            }
        } // end StartOrder();

        // Funktion som ritar ut menyn med alla tillgängliga maträtter
        public void DrawFoodMenu()
        {
            Console.Clear();
            foreach (string lines in MenuLists.FoodMenu)
            {
                Console.WriteLine(lines);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        DrawPizzaMenu();
                        correctKey = true;
                        break;

                    case '2':
                        DrawPastaMenu();
                        correctKey = true;
                        break;
                    case '3':
                        DrawSaladMenu();
                        correctKey = true;
                        break;

                    case '4':
                        DrawDrinksMenu();
                        correctKey = true;
                        break;

                    case '5': // Om listan med ordrar är tom kommer man tillbaka till startskärm
                        if (CurrentOrder.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\nIngen order lagd ännu");
                            System.Threading.Thread.Sleep(2000);
                            DrawFoodMenu();
                        }
                        else
                        {
                            DrawCurrentOrder();
                            correctKey = true;
                        }
                        break;

                    case '6':
                        StartOrder();
                        correctKey = true;
                        break;

                    default:

                        Console.Clear();
                        foreach (string lines in MenuLists.FoodMenu)
                        {
                            Console.WriteLine(lines);
                        }

                        continue;
                }
            }
        }// end DrawFoodMenu();

        // funktion som ritar ut Pizzamenyn
        public void DrawPizzaMenu()
        {
            Console.Clear();
            foreach (string choices in MenuLists.Pizzas)
            {
                Console.WriteLine(choices);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        DrawPizzaBottomMenu("Vesuvio");
                        correctKey = true;
                        break;
                    case '2':
                        DrawPizzaBottomMenu("Margarita");
                        correctKey = true;
                        break;

                    case '3':
                        DrawPizzaBottomMenu("Hawaii");
                        correctKey = true;
                        break;

                    case '4':
                        DrawPizzaBottomMenu("Calzone" + " (Inbakad)");
                        correctKey = true;
                        break;

                    case '5': //Återgå till maträttsmenyn
                        DrawFoodMenu();
                        correctKey = true;
                        break;

                    case '6': //Avsluta
                        StartOrder();
                        correctKey = true;
                        break;

                    default:

                        Console.Clear();
                        foreach (string choices in MenuLists.Pizzas)
                        {
                            Console.WriteLine(choices);
                        }

                        continue;

                }
            }
        } //end drawPizzaMenu();

        // Funktion som låter användaren välja botten och sparar resultatet till pizzalistan
        public void DrawPizzaBottomMenu(string pizza)
        {
            Console.Clear();
            foreach (string choices in MenuLists.PizzaBottoms)
            {
                Console.WriteLine(choices);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        if (pizza == "Vesuvio")
                        {
                            pizzas.Add(new Pizza(pizza));
                            index = pizzas.Count - 1;
                            pizzas[index].AddIngredient(Ingredients[0]); // lägg till tomatsås
                            pizzas[index].AddIngredient(Ingredients[1]); //lägg till ost
                            pizzas[index].AddIngredient(Ingredients[2]); // lägg till skinka
                        }
                        else if (pizza == "Margarita")
                        {
                            pizzas.Add(new Pizza(pizza));
                            index = pizzas.Count - 1;
                            pizzas[index].AddIngredient(Ingredients[0]); // lägg till tomatsås
                            pizzas[index].AddIngredient(Ingredients[1]); //lägg till ost
                        }
                        else if (pizza == "Hawaii")
                        {
                            pizzas.Add(new Pizza(pizza));
                            index = pizzas.Count - 1;
                            pizzas[index].AddIngredient(Ingredients[0]); // lägg till tomatsås
                            pizzas[index].AddIngredient(Ingredients[1]); //lägg till ost
                            pizzas[index].AddIngredient(Ingredients[2]); // lägg till skinka
                            pizzas[index].AddIngredient(Ingredients[8]); // lägg till ananas
                        }
                        else if (pizza == "Calzone" + " (Inbakad)")
                        {
                            pizzas.Add(new Pizza(pizza));
                            index = pizzas.Count - 1;
                            pizzas[index].AddIngredient(Ingredients[0]); // lägg till tomatsås
                            pizzas[index].AddIngredient(Ingredients[1]); //lägg till ost
                            pizzas[index].AddIngredient(Ingredients[2]); // lägg till skinka
                        }
                        CurrentOrder.Add(pizza + ", " + pizzas[index].AddBottom(italiensk));
                        totalPrice += pizzas[index].price;
                        DrawExtras();
                        correctKey = true;
                        break;

                    case '2':
                        if (pizza == "Vesuvio")
                        {
                            pizzas.Add(new Pizza(pizza));
                            index = pizzas.Count - 1;
                            pizzas[index].AddIngredient(Ingredients[0]); // lägg till tomatsås
                            pizzas[index].AddIngredient(Ingredients[1]); //lägg till ost
                            pizzas[index].AddIngredient(Ingredients[2]); // lägg till skinka
                        }
                        else if (pizza == "Margarita")
                        {
                            pizzas.Add(new Pizza(pizza));
                            index = pizzas.Count - 1;
                            pizzas[index].AddIngredient(Ingredients[0]); // lägg till tomatsås
                            pizzas[index].AddIngredient(Ingredients[1]); //lägg till ost
                        }
                        else if (pizza == "Hawaii")
                        {
                            pizzas.Add(new Pizza(pizza));
                            index = pizzas.Count - 1;
                            pizzas[index].AddIngredient(Ingredients[0]); // lägg till tomatsås
                            pizzas[index].AddIngredient(Ingredients[1]); //lägg till ost
                            pizzas[index].AddIngredient(Ingredients[2]); // lägg till skinka
                            pizzas[index].AddIngredient(Ingredients[8]); // lägg till ananas
                        }
                        else if (pizza == "Calzone" + " (Inbakad)")
                        {
                            pizzas.Add(new Pizza(pizza));
                            index = pizzas.Count - 1;
                            pizzas[index].AddIngredient(Ingredients[0]); // lägg till tomatsås
                            pizzas[index].AddIngredient(Ingredients[1]); //lägg till ost
                            pizzas[index].AddIngredient(Ingredients[2]); // lägg till skinka
                        }
                        CurrentOrder.Add(pizza + ", " + pizzas[index].AddBottom(amerikansk));
                        totalPrice += pizzas[index].price;
                        DrawExtras();
                        correctKey = true;
                        break;


                    case '3': //Återgå till Pizzameny
                        DrawPizzaMenu();
                        correctKey = true;
                        break;

                    case '4': //Avsluta
                        StartOrder();
                        correctKey = true;
                        break;

                    default:

                        Console.Clear();
                        foreach (string choices in MenuLists.PizzaBottoms)
                        {
                            Console.WriteLine(choices);
                        }

                        continue;
                }
            }
        } //end DrawPizzaBottomMenu();

        // Funktion som ritar ut en meny med valmöjlighet att bekräfta order eller ändra innehåll
        public void DrawExtras()
        {
            Console.Clear();
            index = pizzas.Count - 1;
            Console.WriteLine(CurrentOrder[CurrentOrder.Count - 1]);
            Console.WriteLine($"~{pizzas[index].ShowIngredients()}~");
            Console.WriteLine($"{pizzas[index].ShowExtraIngredients()}");

            foreach (string lines in MenuLists.ConfirmOrAddExtras)
            {
                Console.WriteLine(lines);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        DrawConfirmationScreen();
                        correctKey = true;
                        break;

                    case '2':
                        DrawAddPieces();
                        correctKey = true;
                        break;

                    case '3':
                        if (pizzas[pizzas.Count - 1].CountIngredients() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Din pizza har inga ingredienser");
                            System.Threading.Thread.Sleep(750);
                            DrawExtras();
                            break;
                        }
                        else
                        {
                            DrawRemovePieces(pizzas.Count - 1);
                            correctKey = true;
                            break;
                        }

                    case '4':
                        CurrentOrder.RemoveAt(CurrentOrder.Count - 1);
                        DrawPizzaMenu();
                        correctKey = true;
                        break;

                    default:
                        Console.Clear();
                        index = pizzas.Count - 1;
                        Console.WriteLine(CurrentOrder[CurrentOrder.Count - 1]);
                        Console.WriteLine($"~{pizzas[index].ShowIngredients()}~");
                        Console.WriteLine($"{pizzas[index].ShowExtraIngredients()}");

                        foreach (string lines in MenuLists.ConfirmOrAddExtras)
                        {
                            Console.WriteLine(lines);
                        }
                        continue;
                }
            }
        } // end DrawExtras();

        // Funktion som ritar ut Pastamenyn
        public void DrawPastaMenu()
        {
            Console.Clear();
            foreach (string choices in MenuLists.Pastas)
            {
                Console.WriteLine(choices);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        pastas.Add(new Pasta("Canneloni med kött"));
                        CurrentOrder.Add("Cannelloni med kött");
                        totalPrice += 80;
                        DrawConfirmationScreen();
                        correctKey = true;
                        break;

                    case '2':
                        pastas.Add(new Pasta("Cannelloni vegetarisk"));
                        CurrentOrder.Add("Cannelloni vegetarisk");
                        totalPrice += 80;
                        DrawConfirmationScreen();
                        correctKey = true;
                        break;

                    case '3':
                        DrawFoodMenu(); //Fortsätt handla
                        correctKey = true;
                        break;

                    case '4': //Återgå till maträttsmenyn
                        StartOrder();
                        correctKey = true;
                        break;

                    default:
                        Console.Clear();
                        foreach (string choices in MenuLists.Pastas)
                        {
                            Console.WriteLine(choices);
                        }
                        continue;
                }
            }
        } //end DrawPastaMenu

        // Funktion som ritar ut Dryckesmenyn
        public void DrawDrinksMenu()
        {
            Console.Clear();
            foreach (string choices in MenuLists.Drinks)
            {
                Console.WriteLine(choices);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        drinks.Add(new Drink("Läsk"));
                        CurrentOrder.Add("Läsk");
                        totalPrice += 20;
                        DrawConfirmationScreen();
                        correctKey = true;
                        break;

                    case '2':
                        drinks.Add(new Drink("Öl"));
                        CurrentOrder.Add("Öl");
                        totalPrice += 50;
                        DrawConfirmationScreen();
                        correctKey = true;
                        break;
                    case '3':
                        drinks.Add(new Drink("Vin"));
                        CurrentOrder.Add("Vin");
                        totalPrice += 50;
                        DrawConfirmationScreen();
                        correctKey = true;
                        break;

                    case '4':
                        DrawFoodMenu(); //Återgår till maträttsmenyn
                        correctKey = true;
                        break;

                    case '5': //Avslutar och återgår till Startmenyn
                        StartOrder();
                        correctKey = true;
                        break;

                    default:
                        Console.Clear();
                        foreach (string choices in MenuLists.Salads)
                        {
                            Console.WriteLine(choices);
                        }
                        continue;
                }
            }
        } //end DrawDrinksMenu();

        // Funktion som ritar ut salladsmenyn
        public void DrawSaladMenu()
        {
            Console.Clear();
            foreach (string choices in MenuLists.Salads)
            {
                Console.WriteLine(choices);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        salads.Add(new Salad("Mozzarellasallad"));
                        CurrentOrder.Add("Mozzarellasallad");
                        totalPrice += 80;
                        DrawConfirmationScreen();
                        correctKey = true;
                        break;

                    case '2':
                        salads.Add(new Salad("Chevresallad"));
                        CurrentOrder.Add("Chevresallad");
                        totalPrice += 80;
                        DrawConfirmationScreen();
                        correctKey = true;
                        break;

                    case '3':
                        DrawFoodMenu(); //Fortsätt handla
                        correctKey = true;
                        break;

                    case '4': //Återgå till maträttsmenyn
                        StartOrder();
                        correctKey = true;
                        break;

                    default:
                        Console.Clear();
                        foreach (string choices in MenuLists.Salads)
                        {
                            Console.WriteLine(choices);
                        }
                        continue;
                }
            }
        } // end DrawSalad

        // Funktion som ritar ut nuvarande order
        public void PrintOrder()
        {
            if(pizzas.Count == 0 && pastas.Count == 0 && salads.Count == 0 && drinks.Count == 0)
            {
                Console.Clear();
                DrawFoodMenu();

            }
            // Listar beställda pizzor
            for (int i = 0; i < pizzas.Count; i++)
            {
                Console.WriteLine($"|*| {pizzas[i].name + ", " + pizzas[i].bottom}");
                Console.WriteLine($"~{pizzas[i].ShowIngredients()}~");
                Console.WriteLine($"{pizzas[i].ShowExtraIngredients()}");
                Console.WriteLine();
            }

            //Listar beställda pastor
            for (int i = 0; i < pastas.Count; i++)
            {
                Console.WriteLine($"|*| {pastas[i].name}");
                Console.WriteLine();
            }

            //Listar beställda sallader
            for (int i = 0; i < salads.Count; i++)
            {
                Console.WriteLine($"|*| {salads[i].name}");
                Console.WriteLine();
            }
            //Lisar beställda drycker
            for (int i = 0; i < drinks.Count; i++)
            {
                Console.WriteLine($"|*| {drinks[i].name}");
                Console.WriteLine();
            }
        } // End Printorder

        // Funktion som ritar ut att en order har lagts till
        public void DrawConfirmationScreen()
        {
            Console.Clear();
            foreach (string lines in MenuLists.ConfirmationScreen)
            {
                Console.WriteLine(lines);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        DrawFoodMenu(); //Fortsätt handla
                        correctKey = true;
                        break;

                    case '2':
                        DrawPayment(); //Gå till betalning
                        correctKey = true;
                        break;

                    case '3':
                        StartOrder(); // Avsluta
                        correctKey = true;
                        break;

                    default:

                        Console.Clear();
                        foreach (string lines in MenuLists.ConfirmationScreen)
                        {
                            Console.WriteLine(lines);
                        }

                        continue;
                }
            }

        } //end DrawConfirmationScreen();

        // Funktion som ritar ut nuvarande order samt totalpris
        public void DrawCurrentOrder()
        {
            Console.Clear();

            Console.WriteLine("Din beställning:");
            Console.WriteLine("-------------\n");

            PrintOrder();

            Console.WriteLine($"\nTotal kostnad: {totalPrice}:-");

            Console.WriteLine("\n-------------\n");

            foreach (string items in MenuLists.CurrentOrderMenu)
            {
                Console.WriteLine(items);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        DrawPayment();
                        correctKey = true;
                        break;

                    case '2':
                        DrawFoodMenu();
                        correctKey = true;
                        break;

                    case '3':
                        DrawRemoveOrder();
                        correctKey = true;
                        break;

                    case '4':
                        StartOrder();
                        correctKey = true;
                        break;

                    default:
                        Console.Clear();

                        Console.WriteLine("Din beställning:");
                        Console.WriteLine("-------------\n");

                        PrintOrder();

                        Console.WriteLine($"\nTotal kostnad: {totalPrice}:-");

                        Console.WriteLine("\n-------------\n");

                        foreach (string items in MenuLists.CurrentOrderMenu)
                        {
                            Console.WriteLine(items);
                        }
                        continue;
                }
            }
        } //end DrawCurrentOrder();

        // Funktion som ritar ut alla tillgängliga extra ingredienser till pizza
        public void DrawAddPieces()
        {
            Console.Clear();
            foreach (string items in MenuLists.Extras)
            {
                Console.WriteLine(items);
            }
            key = Console.ReadKey(true).KeyChar;

            while (correctKey == false)
            {
                switch (key)
                {
                    case '1':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Tomatsås");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '2':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Ost");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '3':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Mozzarella");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '4':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Skinka");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '5':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Salami");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '6':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Lök");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '7':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Oliver");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '8':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Ananas");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '9':
                        index = pizzas.Count - 1;
                        pizzas[index].AddExtraIngredient("Curry");
                        totalPrice += 5;
                        DrawExtras();
                        break;
                    case '0':
                        DrawExtras();
                        break;
                    default:
                        Console.Clear();
                        DrawAddPieces();
                        continue;
                }
            }
        } //end DrawAddPieces();

        // Funktion som ritar ut order och totalkostnad, och låter användaren betala eller gå tillbaka / ändra något
        public void DrawPayment()
        {
            Console.Clear();
            Console.WriteLine("Din beställning:");
            Console.WriteLine("-------------\n");

            PrintOrder();

            Console.WriteLine($"\nTotal kostnad: {totalPrice}:-");
            Console.WriteLine("\n-------------\n");

            foreach (string lines in MenuLists.PaymentMenu)
            {
                Console.WriteLine(lines);
            }

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        bool payment = Payment.Pay(); //Om kund väljer att betala körs en funktion som kontrollerar att betalningen är godkänd
                        Console.Clear();
                        Console.WriteLine("Din betalning behandlas...");
                        System.Threading.Thread.Sleep(1000);
                        if (payment == true)
                        {
                            Console.Clear();
                            DrawReceipt();
                        }
                        else if (payment == false)
                        {
                            Console.Clear();
                            Console.WriteLine("Din betalning godkändes inte, du skickas nu tillbaka till betalningsskärmen...");
                            System.Threading.Thread.Sleep(2000);
                            Console.Clear();
                            DrawPayment();
                        }
                        correctKey = true;
                        break;

                    case '2':
                        DrawFoodMenu(); //Fortsätt handla
                        correctKey = true;
                        break;

                    case '3':
                        DrawCurrentOrder(); // Återgå
                        correctKey = true;
                        break;

                    case '4':
                        StartOrder(); // Avsluta
                        correctKey = true;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Din beställning:");
                        Console.WriteLine("-------------\n");

                        for (int i = 0; i < CurrentOrder.Count; i++)
                        {
                            Console.WriteLine($"|*| {CurrentOrder[i]}");
                            Console.WriteLine($"~{pizzas[i].ShowIngredients()}~");
                            Console.WriteLine($"{pizzas[i].ShowExtraIngredients()}");
                            Console.WriteLine();
                        }

                        Console.WriteLine($"\nTotal kostnad: {totalPrice}:-");
                        Console.WriteLine("\n-------------\n");

                        foreach (string lines in MenuLists.PaymentMenu)
                        {
                            Console.WriteLine(lines);
                        }

                        continue;
                }
            }
        } //end DrawPayment();


        // funktion som skapar ett random ordernumer till kunden
        private int OrderNumber()
        {
            Random ordernumber = new Random();

            return ordernumber.Next(1, 100);
        }

        // Funktion som ritar ut order, totalsumma och ett ordernummer
        public void DrawReceipt()
        {
            Console.WriteLine("Tack för din betalning!");
            Console.WriteLine("-------------\n");


            PrintOrder();

            Console.WriteLine($"\nKostnad: {totalPrice}:-\n");
            Console.WriteLine("-------------\n");
            Console.WriteLine($"Ditt ordernummer är {OrderNumber()}");

            // Programmet startas automatiskt om 10 sekunder efter att kunden tagit emot sitt kvitto
            System.Threading.Thread.Sleep(8000);
            StartOrder();
        } //end DrawReceipt();

        // Funktion som går igenom pizza,pasta,salad och drink list och låter användaren ta bort det valda objektet
        public void DrawRemoveOrder()
        {
            Console.Clear();
            Console.WriteLine("Vilken order vill du ta bort?");
            Console.WriteLine("-------------\n");
            int counter = 0;
                for (int i = 0; i < pizzas.Count + pastas.Count + salads.Count + drinks.Count; i++)
                {
                    
                    if (i >= 0 && i < pizzas.Count)
                    {
                        Console.WriteLine(i + 1 + ". " + pizzas[i].name);
                        Console.WriteLine($"~{pizzas[i].ShowIngredients()}~");
                        Console.WriteLine($"{pizzas[i].ShowExtraIngredients()}");
                        counter++;
                    }

                    else if (i >= pizzas.Count && i < pastas.Count + pizzas.Count)
                    {
                        Console.WriteLine(i + 1 + ". " + pastas[i - pizzas.Count].name);
                        counter++;
                    }

                    else if (i >= pastas.Count + pizzas.Count && i < salads.Count + pastas.Count + pizzas.Count)
                    {
                        Console.WriteLine(i + 1 + ". " + salads[i - (pizzas.Count + pastas.Count)].name);
                        counter++;
                    }
                    else if (i >= salads.Count + pastas.Count + pizzas.Count && i < drinks.Count + salads.Count + pastas.Count + pizzas.Count)
                    {
                        Console.WriteLine(i + 1 + ". " + drinks[i - (pizzas.Count + salads.Count + pastas.Count)].name);
                        counter++;
                    }
                
                }
            Console.WriteLine($"\n{counter +1}. Gå tillbaka");
                while (correctKey == false)
                {
                    key = Console.ReadKey(true).KeyChar;
                    int keyInput = key - '0'; // Gör om Char till motsvarande siffra i int

                    //pizzas
                    if(pizzas.Count > 0 && keyInput <= pizzas.Count)
                    {
                        totalPrice -= pizzas[keyInput - 1].price;
                        pizzas.RemoveAt(keyInput-1);
                        DrawConfirmRemoval();
                        correctKey = true;
                    }

                    //pastas
                    if(keyInput > pizzas.Count && keyInput <= pastas.Count + pizzas.Count)
                    {
                        totalPrice -= pastas[keyInput - pizzas.Count - 1].price;
                        pastas.RemoveAt((keyInput - pizzas.Count) - 1);
                        DrawConfirmRemoval();
                        correctKey = true;
                    }

                    //salads
                    if(keyInput > pastas.Count + pizzas.Count && keyInput <= salads.Count + pastas.Count + pizzas.Count)
                    {
                        totalPrice -= salads[keyInput - (pizzas.Count + pastas.Count) - 1].price;
                        salads.RemoveAt(keyInput - (pizzas.Count + pastas.Count) - 1);
                        DrawConfirmRemoval();
                        correctKey = true;
                    }
                    //drinks
                    if (keyInput > salads.Count + pastas.Count + pizzas.Count && keyInput <= drinks.Count + salads.Count + pastas.Count + pizzas.Count)
                    {
                        totalPrice -= drinks[keyInput - (pizzas.Count + pastas.Count + salads.Count) - 1].price;
                        drinks.RemoveAt(keyInput - (pizzas.Count + pastas.Count + salads.Count) - 1);
                        DrawConfirmRemoval();
                        correctKey = true;
                    }
                    if(keyInput == counter+1)
                    {
                        DrawCurrentOrder();
                    }
                    if(keyInput > drinks.Count + salads.Count + pastas.Count + pizzas.Count)
                    {
                        continue;
                    }
                }
        } //end DrawRemoveOrder();

        // Funktion som går igenom pizza listan och skriver ut innehåll + ingredienser, användaren får välja vilken ingrediens som ska tas bort
        public void DrawRemovePieces(int index)
        {
            Console.Clear();
            Console.WriteLine("Vilken ingrediens vill du ta bort?");
            Console.WriteLine("-------------\n");

            int ingredientCounter = 0;

            foreach(string ingredient in pizzas[index].IngredientList)
            {
                ingredientCounter++;
                Console.WriteLine(ingredientCounter + ". " + ingredient);
            }

            if(pizzas[index].ExtraIngredientList.Count > 0)
            {
                foreach(string extraIngredient in pizzas[index].ExtraIngredientList)
                {
                    ingredientCounter++;
                    Console.WriteLine(ingredientCounter + ". " + extraIngredient);
                }
            }

            Console.WriteLine("\n" + (ingredientCounter+1) + ". " + "Ångra");

            while (correctKey == false)
            {
                key = Console.ReadKey(true).KeyChar;
                int userChoice = key - '0';

                for(int i = 0; i <= ingredientCounter; i++)
                {
                    // ta bort basingredienser
                    if(userChoice == i && userChoice <= pizzas[index].IngredientList.Count)
                    {
                        ConfirmRemoval(pizzas[index].IngredientList, index, userChoice, "normal");
                    }

                    // ta bort extra ingredienser
                    else if(userChoice == i && userChoice > pizzas[index].IngredientList.Count)
                    {
                        ConfirmRemoval(pizzas[index].ExtraIngredientList, index, (userChoice - pizzas[index].IngredientList.Count), "extra");
                    }

                    else if(userChoice == ingredientCounter + 1)
                    {
                        DrawExtras();
                    }
                }

            }
        } //end DrawRemovePieces();

        public void ConfirmRemoval(List<string> IngredientList, int pizzaIndex, int ingredientIndex, string normalOrExtra)
        {
            bool correctKey = false;

            while (correctKey == false)
            {
                Console.Clear();
                Console.WriteLine($"Är du säker på att du vill ta bort {IngredientList[ingredientIndex - 1]}?");
                Console.WriteLine("1. Ja");
                Console.WriteLine("2. Nej");

                int userChoice = Console.ReadKey(true).KeyChar - '0';

                if (userChoice == 1 && normalOrExtra == "normal")
                {
                    pizzas[pizzaIndex].RemoveIngredient(ingredientIndex);
                    DrawExtras();
                }

                else if (userChoice == 1 && normalOrExtra == "extra")
                {
                    pizzas[pizzaIndex].RemoveExtraIngredient(ingredientIndex);
                    totalPrice -= 5;
                    DrawExtras();
                }

                else if (userChoice == 2)
                {
                    DrawExtras();
                }
            }

        }

        // Funktion som ritar ut att ett val har tagits bort
        public void DrawConfirmRemoval()
        {
            Console.Clear();
            Console.WriteLine("Ditt val är borttaget");
            Console.WriteLine("-------------\n");

            System.Threading.Thread.Sleep(1500);

            DrawCurrentOrder();
        }
    }
} //end class
