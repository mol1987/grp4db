using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Loading
{
    public class SpinningText
    {
        string loadingString = "Loading       ";
        string loadingCharacter = @"-/-\";
        int origRow;
        int origCol;
        int incr = 0;
        ConsoleColor foreground = Console.ForegroundColor;

        public void Stop()
        {

        }
        public void PrintLoop()
        {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            Console.CursorVisible = false;

            while (true)
            {
                PrintLoadingScreen();
                try

                {

                    Thread.Sleep(100);

                }

                catch (ThreadInterruptedException)
                {
                    Console.Clear();
                    Console.ForegroundColor = foreground;
                    Console.CursorVisible = true;
                    //Console.SetCursorPosition(origCol, origRow);
                    return;

                }
                
                loadingString = ShiftString(loadingString);
            }
        }

        void WriteAt(char s, int x, int y)
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
            catch (ThreadInterruptedException)
            {
                Console.Clear();
                Console.ForegroundColor = foreground;
                Console.CursorVisible = true;
                //Console.SetCursorPosition(origCol, origRow);
                return;
            }
        }

        void PrintLoadingScreen()
        {
            int nr = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            WriteAt(loadingString[nr++], 4, 0);
            WriteAt(loadingString[nr++], 5, 0);
            WriteAt(loadingString[nr++], 6, 0);

            WriteAt(loadingString[nr++], 7, 1);
            WriteAt(loadingString[nr++], 8, 2);
            WriteAt(loadingString[nr++], 7, 3);
            WriteAt(loadingString[nr++], 6, 4);

            WriteAt(loadingString[nr++], 5, 4);
            WriteAt(loadingString[nr++], 4, 4);
            WriteAt(loadingString[nr++], 3, 4);

            WriteAt(loadingString[nr++], 2, 3);
            WriteAt(loadingString[nr++], 1, 2);
            WriteAt(loadingString[nr++], 2, 1);
            WriteAt(loadingString[nr++], 3, 0);
            Console.ForegroundColor = (System.ConsoleColor)(incr % 15);
            WriteAt(loadingCharacter[incr++ % loadingCharacter.Length], 4, 2);
            Console.ForegroundColor = foreground;
        }
        public static string ShiftString(string t)
        {
            return t.Substring(t.Length - 1, 1) + t.Substring(0, t.Length - 1);
        }
    }
}
