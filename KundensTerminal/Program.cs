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
        static int origRow, origCol;
        static async Task Main(string[] args)
        {
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            // kund terminal 
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
                List<Orders> allProcessing = allOrders.Where(x => x.Orderstatus == 1).ToList();
                thr.Interrupt();
                Thread.Sleep(200);
                Console.Clear();
                int cx = 0, cy = 2;
                Console.BackgroundColor = ConsoleColor.Blue;

                WriteAt("Pågående ordrar | Klara ordrar", 0, 0);
                Console.BackgroundColor = ConsoleColor.Black;
                WriteAt("------------------------------", 0, 1);
                allProcessing.ForEach(x => WriteAt("" + x.ID, 6, cy++));
                cy = 2;
                allProcessing.ForEach(x => WriteAt("|", 16, cy++));
                cy = 2;
                allDoneOrders.ForEach(x => WriteAt("" + x.ID, 23, cy++));
                Console.SetCursorPosition(origCol, origRow);
                System.Threading.Thread.Sleep(2000);
            }
        }

        static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        static void loading()
        {
            SpinningText sp = new SpinningText();
            sp.PrintLoop();
        }
    }
}





