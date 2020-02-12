using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MsSqlRepo;
using TypeLib;

namespace OrderTerminal_JC
{
    class Program
    {
        public class ExThread
        {
            Orders order = new Orders();
            List<Orders> allOrders;
            List<Orders> allDoneOrders;

            public async void thread1()
            {
                while (true)
                {
                    Console.Clear();
                    allOrders = (await General.ordersRepo.GetAllAsync()).ToList();
                    allDoneOrders = allOrders.Where(x => x.Orderstatus == 2).ToList();
                    Console.WriteLine(" foljande orderar finns redo att hämta ut");
                    allDoneOrders.ForEach(x => Console.WriteLine(x.ID + " " + "\n" + "------"));
                    Console.WriteLine(" Ange numret för den order som  gästen  hämtat");

                    System.Threading.Thread.Sleep(2000);
                }
            }

            // Non-static method 
            public void thread2()
            {
                int choice;

                while (true)
                {
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

                    General.ordersRepo.UpdateAsync(order);
                }
            }
        }
        static async Task Main(string[] args)
        {
            bool res = Helper.Environment.LoadEnvFile() ? true : false;

            // Creating object of ExThread class 
            ExThread obj = new ExThread();
            // Creating thread 
            // Using thread class 
            Thread thr = new Thread(new ThreadStart(obj.thread1));
            thr.Start();
            Thread thr2 = new Thread(new ThreadStart(obj.thread2));
            thr2.Start();
        }

    }
}
