using System;
using System.Collections.Generic;
using System.Text;
using TypeLib;

namespace MenuTestSystem.Menu
{
    class MenuCode
    {
        public Articles WorkingArticle = new Articles();
        public ExitMenu exitMenu = new ExitMenu();
        public MainMenu mainMenu = new MainMenu();
        public IngredientsPage ingredientsPage = new IngredientsPage();
        public PizzaMenu pizzaMenu = new PizzaMenu();
        public ShowArticle showArticle = new ShowArticle();
        public ConfirmOrderPage confirmOrderPage = new ConfirmOrderPage();
    }
}
