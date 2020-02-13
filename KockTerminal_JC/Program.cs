using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using MsSqlRepo;
using TypeLib;

namespace KockTerminal_JC
{
    class Program
    {
        private static bool isReady = true;
        public static int Counter = 16;
        public static string dots = "";
        private static Timer aTimer;
        public static List<DisplayObject> Objects = new List<DisplayObject>();
        static async Task Main(string[] args)
        {
            Console.Title = "Kock";// Changes title
            bool res = Helper.Environment.LoadEnvFile() ? true : false; // Config stuff
            bool isRunning = true;
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += Increment;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            while (isRunning)
            {
                if (Console.KeyAvailable)
                {
                    int choice;
                    aTimer.Stop();
                    string temp = Console.Title;
                    Console.Title = "STOPPED";
                    string input = Console.ReadLine();
                    aTimer.Start();
                    if (Int32.TryParse(input, out choice) && Objects.Exists(obj => obj.ID == choice))
                    {
                        Objects.Find(obj => obj.ID == choice).Orderstatus = 2;
                    }
                    RefreshView();
                    Console.Title = temp;
                }
                if (Counter >= 16)
                {
                    UpdateRows();
                    Counter = 0;
                }
                // isRunning = false;
                System.Threading.Thread.Sleep(20);
            }
        }
        private static void Increment(Object source, System.Timers.ElapsedEventArgs e)
        {
            Counter += 1;
            if (Counter >= dots.Length) dots += ".";
            else dots = "";
            WriteToTitle();
        }
        private static void WriteToTitle()
        {
            Console.Title = String.Format("Kock[Updates in {0}s]{1}", 16 - Counter, dots);
        }
        private static void RefreshView()
        {
            int row = 0;
            Console.SetCursorPosition(0, 0);
            Objects.First().PrintKeys();
            foreach (var obj in Objects)
            {
                row += 1;
                if (row <= 20)
                {
                    obj.Print();
                    continue;
                }
                string xx = Objects.Count.ToString();
                Console.WriteLine("+ " + xx + " more articles");
                Console.WriteLine("");
                break;
            }
        }
        private static async void UpdateRows()
        {
            // HERE UPDATE ROWS

            Objects.Where(obj => obj.Orderstatus == 2).ToList().ForEach(async obj =>
            {
                Orders res = new Orders();
                res = await General.ordersRepo.GetAsync(obj.OrdersID);
                res.Orderstatus = 2;
                await General.ordersRepo.UpdateAsync(res);
            });
            Objects = SortObjects((await GetOrdersBy(1)));
            RefreshView();
        }
        private static async Task<List<DisplayObject>> GetOrdersBy(int n) => (await General.ordersRepo.GetAllAsync(1)).ToList();
        private static List<DisplayObject> SortObjects(List<DisplayObject> objects)
        {
            List<DisplayObject> filteredObjects = new List<DisplayObject>();
            foreach (var obj in objects)
            {
                if (filteredObjects.Exists(fobj => fobj.ID == obj.ID))
                    filteredObjects.Find(fobj => fobj.ID == obj.ID).Ingredient += obj.Ingredient;
                else
                    filteredObjects.Add(obj);
            }
            return filteredObjects;
        }
    }
}
