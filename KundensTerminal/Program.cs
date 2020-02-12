using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MsSqlRepo;
using TypeLib;
using Loading;

namespace KundensTerminal
{
    class Program
    {
        
        static async Task Main(string[] args)
        {// kund terminal 
            /// Change the Window titles
            Console.Title = "Kund";
            bool res = Helper.Environment.LoadEnvFile() ? true : false;
            Thread thr = new Thread(new ThreadStart(loading));
            thr.Start();
            //thr.Abort();
            while (true)
            {
                
                List<Orders> allOrders = (await General.ordersRepo.GetAllAsync()).ToList();
                List<Orders> allDoneOrders = allOrders.Where(x => x.Orderstatus == 2).ToList();
                thr.Interrupt();
                Console.Clear();
                allDoneOrders.ForEach(x => Console.WriteLine(x.ID + " " + x.TimeCreated));
                System.Threading.Thread.Sleep(2000);
            }

        }

        static void loading()
        {
            SpinningText sp = new SpinningText();
            sp.PrintLoop();
        }
    }
}





