﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsSqlRepo;
using TypeLib;

namespace BeställningsTerminal.Menu
{
    public class ShowArticle : IMenu
    {
        public string Name { get; set; }
        public List<IMenu> PagesList { get; set; }
        public ShowArticle()
        {
            Name = "ShowArticle";
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.ingredientsPage);
            PagesList.Add(Globals.confirmOrderPage);
        }

        public async Task Print()
        {
            Console.Clear();
            int no = 1;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(Globals.WorkingArticle.Name);
            Console.BackgroundColor = ConsoleColor.Black;
            Globals.WorkingArticle.Ingredients.ForEach(x => Console.Write(x.Name + " "));
            Console.WriteLine("\n");
            Console.BackgroundColor = ConsoleColor.Blue;
            PagesList.ForEach(x => Console.WriteLine(no++ + ". " + x.Name));
            Console.BackgroundColor = ConsoleColor.Black;
            int choice;
            int.TryParse(Console.ReadLine(), out choice);
            if (PagesList != null)
                await PagesList[choice-1].Print();
        }
    }
}
