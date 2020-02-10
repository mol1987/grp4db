using System;
using System.Collections.Generic;
using System.Text;
using TypeLib;

namespace MenuTestSystem.Menu
{
    public static class Globals
    {
        public static Articles WorkingArticle = new Articles();
        public static ExitMenu exitMenu = new ExitMenu();       
        
        public static ConfirmOrderPage confirmOrderPage = new ConfirmOrderPage();
        public static IngredientsPage ingredientsPage = new IngredientsPage();
        public static ShowArticle showArticle = new ShowArticle();
        public static PizzaMenu pizzaMenu = new PizzaMenu();
        public static MainMenu mainMenu = new MainMenu();
    }
}