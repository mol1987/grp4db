using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MsSqlRepo;
using TypeLib;

namespace OrderTerminal_JC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            bool res = Helper.Environment.LoadEnvFile() ? true : false;

            while (true)
            {

                Console.Clear();

                List<Orders> allOrders = (await General.ordersRepo.GetAllAsync()).ToList();

                List<Orders> allDoneOrders = allOrders.Where(x => x.Orderstatus == 2).ToList();
                Console.WriteLine(" foljande orderar finns redo att hämta ut");

                allDoneOrders.ForEach(x => Console.WriteLine(x.ID + " " + "\n" + "------"));
                Console.WriteLine(" Ange numret för den order som  gästen  hämtat");



                System.Threading.Thread.Sleep(2000);


                Orders order = new Orders();

                int choice = 3;

                int.TryParse(Console.ReadLine(), out choice);
                foreach (var item in allDoneOrders)
                {
                    if (item.ID == choice)
                    {
                        item.Orderstatus = 3;
                        order = item;

                        break;

                    }

                }

                await General.ordersRepo.UpdateAsync(order);


            }


        }
    }
}
