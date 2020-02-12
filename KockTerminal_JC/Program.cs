using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MsSqlRepo;
using TypeLib;

namespace KockTerminal_JC
{
    class Program
    {
        //public static Orders order;

        static async Task Main(string[] args)
        {
            Orders order = new Orders();
            bool res = Helper.Environment.LoadEnvFile() ? true : false;
            while (true)
            {
                List<Orders> allOrders = (await General.ordersRepo.GetAllAsync()).ToList();
                List<Articles> articles = new List<Articles>();


                List<Orders> beginningOrder = allOrders.Where(x => x.Orderstatus == 0).ToList();
                Console.WriteLine("välig den order som du vill tillaga");

                Console.WriteLine(" ");
                foreach (Orders orderItem in beginningOrder)
                {
                    orderItem.Articles = (await General.articlesRepo.GetAllAsync(orderItem)).ToList();
                    Console.Write(orderItem.ID + " ");
                    orderItem.Articles.ForEach(x => Console.WriteLine(x.Name));

                }

                int choice = 62;
                int.TryParse(Console.ReadLine(), out choice);

                foreach (var items in beginningOrder)
                {
                    if (items.ID == choice)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine(" den order som du väljat inhåller följande ingredienter");
                        Console.WriteLine(" ");
                        foreach (var item in items.Articles)
                        {
                            Console.WriteLine(item.Name);

                            foreach (Ingredients itemIngredients in item.Ingredients)
                            {

                                Console.WriteLine(itemIngredients.Name);
                                break;
                            }


                        }


                    }
                }
                Console.WriteLine("--------------");
                Console.WriteLine(" ");
                Console.WriteLine(" välja 1: för stopa in i ugn " + "\n" + "välja 2:Återgå");
                int selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 1)
                {
                    List<Orders> allDoneOrders = allOrders.Where(x => x.Orderstatus == 2).ToList();
                    Console.WriteLine(" ");
                    Console.WriteLine(" foljande orderar finns redo att hämta ut");

                    allDoneOrders.ForEach(x => Console.WriteLine(x.ID + " " + "\n" + "------"));
                    Console.WriteLine(" Ange numret för den order som  gästen  hämtat");

                    break;
                }







            }










            //    static void Main(string[] args)
            //    {
            //        List<Food> orders = new List<Food>();

            //        GetOrders(orders);
            //    }

            //    // denna funktion används för att rita ut en startskärm
            //    // funktionen där in en lista med de ordrar som inkommit så att ordrarna ständigt finns sparade
            //    public static void GetOrders(List<Food> orders)
            //    {
            //        int orderCounter, userInput;
            //        bool clicked = false;

            //        Console.Clear();

            //        while (clicked == false)
            //        {
            //            Console.WriteLine("Välj den order som du vill tillaga");
            //            Console.WriteLine("-------------\n");

            //            orderCounter = 0;

            //            for (int i = 0; i < 5; i++)
            //            {
            //                orderCounter++;
            //                orders.Add(new Food());
            //                orders[orders.Count - 1].GenerateFood();
            //                Console.WriteLine(orderCounter + ". " + orders[orderCounter - 1].name);
            //                System.Threading.Thread.Sleep(500);
            //            }

            //            userInput = Console.ReadKey(true).KeyChar - '0';
            //                if (userInput > 0 && userInput < 6)
            //                {
            //                    orders[userInput - 1].ShowOrder(orders[userInput - 1].name, orders, userInput - 1);
            //                    clicked = true;
            //                }

            //            Console.Clear();
            //        }
            //    }

        }
    }
}
