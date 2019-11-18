using System;
using System.Collections.Generic;

namespace OrderTerminal_JC
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> ordersReadyToCollect = new List<int>();

            Start(ordersReadyToCollect);
        }

        // Startskärm när restaurangen är stängd och även funktion som sätter igång hela systemet
        public static void Start(List<int> ordersReadyToCollect)
        {
            bool correctKey = false;


            while (correctKey == false)
            {
                Console.Clear();
                Console.WriteLine("Klicka Enter för att öppna kassasystemet");
                char key = Console.ReadKey(true).KeyChar;

                if (key == 13)
                {
                    OrdersReadyToServe(ordersReadyToCollect);
                    correctKey = true;
                }
            }
        }

        // denna funktion skriver ut de ordrar som finns att hämta
        public static void OrdersReadyToServe(List<int> ordersReadyToCollect)
        {
            bool chosenOrder = false;
            bool correctKey = false;

            // programmet kommer att återupprepas tilsl dess att kassapersonen skriver in en giltig order
            while (chosenOrder == false)
            {
                while (correctKey == false)
                {
                    Console.Clear();
                    Console.WriteLine("Följande ordrar finns redo att hämta ut:\n");

                    for (int i = 0; i < 5; i++)
                    {
                        if (ordersReadyToCollect.Count < 5)
                        {
                            // så länge antalaet ordrar är färre än 5 så simuleras det nya färdiga ordrar
                            ordersReadyToCollect.Add(GetOrderNumber());

                            // om det är mer än 1 order gjord så görs även en kontroll så att ordernumren inte är likadana
                            // om detta är fallet så tas numret bort och loopen fortsätter tills dess att det finns fem unika nummer
                            if (ordersReadyToCollect.Count > 1 && i > 0)
                            {
                                ordersReadyToCollect.Sort();
                                if (ordersReadyToCollect[i] == ordersReadyToCollect[i - 1])
                                {
                                    ordersReadyToCollect.RemoveAt(i);
                                }
                            }
                        }
                    }

                    // här skrivs sedan ordrarna ut
                    for (int i = 0; i < ordersReadyToCollect.Count; i++)
                    {

                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("  " + ordersReadyToCollect[i]);
                        Console.WriteLine("------");
                    }

                    // och kassapersonalen ombeds skriva in den order som en kund hämtar
                    Console.WriteLine("Ange numret på den order som gästen hämtat: ");
                    correctKey = int.TryParse(Console.ReadLine(), out int orderNumber);

                    // här kontrolleras sedan så att det nummer som skrivs in överensstämmer med det som angetts
                    foreach (int order in ordersReadyToCollect)
                    {
                        if (order == orderNumber)
                        {
                            Console.Clear();
                            Console.WriteLine("Ordern utlämnad");
                            ordersReadyToCollect.Remove(order);
                            System.Threading.Thread.Sleep(1500);
                            OrdersReadyToServe(ordersReadyToCollect);
                            chosenOrder = true;
                        }
                    }

                }
            }
        }

        // denna funktion skapar ett randomiserat ordernummer mellan 1-100
        public static int GetOrderNumber()
        {
            Random order = new Random();
            int orderNumber = order.Next(1, 100);

            return orderNumber;
        }
    }
}
