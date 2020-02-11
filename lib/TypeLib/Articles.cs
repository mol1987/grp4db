
using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLib
{
    public class Articles
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? BasePrice { get; set; }
        public string? Type { get; set; }
        public ArticleOrders? ArticleOrder { get; set; }

        public List<Ingredients>? Ingredients { get; set; }

        
      
        /// <summary>
        /// Prints out neatly for list
        /// </summary>
        public void PrintRow()
        {

            Console.WriteLine("..........................");

            Console.Write("{0} {1} {2} {3} \n", ID, Name.PadLeft(4), BasePrice.ToString().PadLeft(4), Type.PadLeft(4));
        }
        public void PrintKeys()
        {
            Console.Write("{0} {1}  {2} {3} \n", "ID" , "Name" ,"BasePrice" , "Type");
        }

        public Articles Clone()
        {
            if (this.Ingredients == null) this.Ingredients = new List<Ingredients>();
            if (this.ArticleOrder == null) this.ArticleOrder = new ArticleOrders();
            return new Articles { ID = this.ID, ArticleOrder = (ArticleOrders)this.ArticleOrder.Clone(), BasePrice = this.BasePrice, Ingredients = this.Ingredients.GetRange(0, this.Ingredients.Count), Name = this.Name, Type = this.Type };
        }
    }
}
