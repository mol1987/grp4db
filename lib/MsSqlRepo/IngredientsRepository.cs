using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace MsSqlRepo
{
    public class IngredientsRepository : GenericRepository<Ingredients>
    {
        public IngredientsRepository(string tableName) : base(tableName)
        {
           
        }
        public async Task InsertCustomIngredientsAsync(Orders order, Articles article, List<Ingredients> ingredients)
        {
            using (var connection = CreateConnection())
            {
                foreach (var ingredient in ingredients)
                {
                    var insertQuery = $"INSERT INTO ArticleOrdersIngredients (IngredientsID, ArticlesID, OrdersID) VALUES (@OrdersID, @ArticlesID, @IngredientsID)";
                    await connection.ExecuteAsync(insertQuery, new ArticleOrdersIngredients {IngredientsID = (int)ingredient.ID, ArticlesID = (int)article.ID, OrdersID = (int) order.ID});
                
                }
            }

        }
    }
}
