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
        private static async Task<List<DisplayObject>> GetOrdersBy(int n)
        {
            List<DisplayObject> orders = (await General.ordersRepo.GetAllAsync(1)).ToList();
            //foreach(Orders order in orders)
            //{
            //    order.Articles = new List<Articles>();
            //    var articles = (await General.articlesRepo.GetAllAsync(order)).ToList();
            //    articles.ForEach(article => order.Articles.Add(article));
            //}
            return orders;
        }
        static async Task Main(string[] args)
        {
            Console.Title = "Kock";// Changes title
            bool res = Helper.Environment.LoadEnvFile() ? true : false; // Config stuff
            bool isRunning = true;
            int maxRows = Console.LargestWindowHeight;
            int x = 0;

            List<DisplayObject> orders = (await GetOrdersBy(x));

            while (isRunning)
            {
                if (Console.KeyAvailable)
                {
                    Console.WriteLine("input detected");
                    string input = Console.ReadLine();
                    Console.WriteLine(input + "xxx");
                }
                Console.WriteLine("ID   Name    OrderID     OrderStatus     Ingredient");
                orders.ForEach(order => {
                    Console.WriteLine("{0}{1}{2}{3}{4}",
                        order.ID.ToString().PadLeft(4),
                        order.Name.PadLeft(14),
                        order.OrderID.ToString().PadLeft(4),
                        order.Orderstatus.ToString().PadLeft(4),
                        order.Ingredient.PadLeft(14)
                   );
                });
                // remove me

                Console.WriteLine("Remove me--");
                Console.ReadLine();
                isRunning = false;
                System.Threading.Thread.Sleep(20);
            }



            //Orders order = new Orders();
            //
            //while (true)
            //{
            //    List<Orders> allOrders = (await General.ordersRepo.GetAllAsync()).ToList();
            //    List<Articles> articles = new List<Articles>();

            //    List<Orders> beginningOrder = allOrders.Where(x => x.Orderstatus == 0).ToList();
            //    Console.WriteLine("välje den order som du vill tillaga");

            //    Console.WriteLine(" ");
            //    foreach (Orders orderItem in beginningOrder)
            //    {
            //        orderItem.Articles = (await General.articlesRepo.GetAllAsync(orderItem)).ToList();
            //        Console.Write(orderItem.ID + " ");

            //        orderItem.Articles.ForEach(x => Console.WriteLine(x.Name));

            //    }

            //    int choice = 62;
            //    int.TryParse(Console.ReadLine(), out choice);

            //    foreach (var items in beginningOrder)
            //    {
            //        if (items.ID == choice)
            //        {
            //            Console.WriteLine(" ");
            //            Console.WriteLine("Den order som du valt inhåller följande ingredienser");
            //            Console.WriteLine(" ");
            //            foreach (var item in items.Articles)
            //            {
            //                Console.WriteLine(item.Name);

            //                foreach (Ingredients itemIngredients in item.Ingredients)
            //                {
            //                    Console.WriteLine(itemIngredients.Name);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    Console.WriteLine("--------------");
            //    Console.WriteLine(" ");
            //    Console.WriteLine(" välja 1: för stopa in i ugnen " + "\n" + "välja 2: Återgå");
            //    int selection = Convert.ToInt32(Console.ReadLine());
            //    if (selection == 1)
            //    {
            //        List<Orders> allDoneOrders = allOrders.Where(x => x.Orderstatus == 2).ToList();
            //        Console.WriteLine(" ");
            //        Console.WriteLine(" foljande orderar finns redo att hämtas ut");

            //        allDoneOrders.ForEach(x => Console.WriteLine(x.ID + " " + "\n" + "------"));
            //        Console.WriteLine(" Ange numret för den order som  gästen  hämtat");

            //        break;
            //    }
            //}
        }
    }
}
