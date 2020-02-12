﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TypeLib;
using Dapper;
using System.Threading.Tasks;

namespace MenuTestSystem.Menu
{
    public class MainMenu : IMenu
    {
        public string Name { get; set; }
        int no = 1;
        int choice = 0;
        public List<IMenu> PagesList { get; set; }
        public MainMenu()
        {
            Name = "MainMenu";   
        }
        public async Task Print()
        {
            no = 1;
            PagesList = new List<IMenu>();
            PagesList.Add(Globals.pizzaMenu);
            PagesList.Add(Globals.showOrder);
            PagesList.Add(Globals.exitMenu);
            Console.Clear();
            Console.WriteLine("Choose a category!");
            PagesList.ForEach(x => Console.WriteLine(no++ + ". " + x.Name));
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice <= 0 || choice > PagesList.Count);
            await PagesList[choice - 1].Print();
        }
    }
}