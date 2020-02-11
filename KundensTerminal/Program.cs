using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MsSqlRepo;
using TypeLib;

namespace KundensTerminal
{
    class Program
    {
        static async Task Main(string[] args)
        {// kund terminal 
            bool res = Helper.Environment.LoadEnvFile() ? true : false;
            while (true)
            {

                Console.Clear();
                List<Orders> allOrders = (await General.ordersRepo.GetAllAsync()).ToList();

                List<Orders> allDoneOrders = allOrders.Where(x => x.Orderstatus == 2).ToList();
                allDoneOrders.ForEach(x => Console.WriteLine(x.ID + " " + x.TimeCreated));
                System.Threading.Thread.Sleep(2000);
            }

        }


    }
}





