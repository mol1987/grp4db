using System;
using System.Collections.Generic;
using System.Threading;

namespace KundensTerminal
{
    class Program
    {
        private static List<int> QView;

        static void Main(string[] args)
        {
            QView = new List<int>();

            while (true)
            {
                LoopV2();
            }
        }

        private static void LoopV2()
        {
            Console.Clear();
            Console.WriteLine("Tack för er beställning \n" + "----------------------");

            Console.WriteLine("Hämta order nr. ");
            foreach (int item in QView)
            {
                Console.WriteLine(item);
            }
            Thread.Sleep(5000);

            Random random = new Random();
            int OLdOrderNr = 0;
            int OrderNr = 0;
            OLdOrderNr = QView.Find(x => x == OrderNr);
            OrderNr = random.Next(1, 100);

            if (OLdOrderNr == 0)
            {
                for (int i = 0; i < QView.Count - 1; i++)
                {
                    if (QView.Count > 4)
                    {
                        QView.RemoveAt(QView.Count - 5);
                    }
                }

                OLdOrderNr = 0;
                QView.Add(OrderNr);
            }

        }

    }
}





