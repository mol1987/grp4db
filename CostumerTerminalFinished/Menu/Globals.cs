using System;
using System.Collections.Generic;
using System.Text;
using TypeLib;

namespace BeställningsTerminal.Menu
{
    public static class Globals
    {
        public static List<Articles> basketArticles = new List<Articles>();
        public static Articles WorkingArticle = new Articles();

        public static Reciept reciept = new Reciept();
        public static ExitMenu exitMenu = new ExitMenu();
        public static FinalizeOrder finalizeOrder = new FinalizeOrder();
        public static ConfirmOrderPage confirmOrderPage = new ConfirmOrderPage();
        public static IngredientsPage ingredientsPage = new IngredientsPage();
        public static ShowArticle showArticle = new ShowArticle();
        public static ShowOrder showOrder = new ShowOrder();
        public static PizzaMenu pizzaMenu = new PizzaMenu();
        public static MainMenu mainMenu = new MainMenu();
    }
}