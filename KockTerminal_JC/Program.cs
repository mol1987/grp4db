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
            // Changes title
            Console.Title = "Kock";
            
            Orders order = new Orders();
            bool res = Helper.Environment.LoadEnvFile() ? true : false;
            while (true)
            {
                List<Orders> allOrders = (await General.ordersRepo.GetAllAsync()).ToList();
                List<Articles> articles = new List<Articles>();

                List<Orders> beginningOrder = allOrders.Where(x => x.Orderstatus == 0).ToList();
                Console.WriteLine("välje den order som du vill tillaga");

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
                        Console.WriteLine("Den order som du valt inhåller följande ingredienser");
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
                Console.WriteLine(" välja 1: för stopa in i ugnen " + "\n" + "välja 2: Återgå");
                int selection = Convert.ToInt32(Console.ReadLine());
                if (selection == 1)
                {
                    List<Orders> allDoneOrders = allOrders.Where(x => x.Orderstatus == 2).ToList();
                    Console.WriteLine(" ");
                    Console.WriteLine(" foljande orderar finns redo att hämtas ut");

                    allDoneOrders.ForEach(x => Console.WriteLine(x.ID + " " + "\n" + "------"));
                    Console.WriteLine(" Ange numret för den order som  gästen  hämtat");

                    break;
                }
            }
        }

        
    }
}
