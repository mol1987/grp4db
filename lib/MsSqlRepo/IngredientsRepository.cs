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
        string tableName;
        public IngredientsRepository(string tableName) : base(tableName)
        {
            this.tableName = tableName;
        }
        public async Task InsertCustomIngredientsAsync(ArticleOrders articleOrder, List<Ingredients> ingredients)
        {
            using (var connection = CreateConnection())
            {
                foreach (var ingredient in ingredients)
                {
                    var insertQuery = $"INSERT INTO ArticleOrdersIngredients (IngredientsID, ArticleOrdersID) VALUES (@IngredientsID, @ArticleOrdersID)";
                    await connection.ExecuteAsync(insertQuery, new ArticleOrdersIngredients { IngredientsID = (int)ingredient.ID, ArticleOrdersID = articleOrder.ID });
                }
            }

        }
        /*
        public async Task GetCustomIngredientsAsync(ArticleOrders articleOrder, List<Ingredients> ingredients)
        {
            using (var connection = CreateConnection())
            {
                foreach (var ingredient in ingredients)
                {
                    var insertQuery = $"INSERT INTO ArticleOrdersIngredients (IngredientsID, ArticleOrdersID) VALUES (@IngredientsID, @ArticleOrdersID)";
                    await connection.ExecuteAsync(insertQuery, new ArticleOrdersIngredients { IngredientsID = (int)ingredient.ID, ArticleOrdersID = articleOrder.ID });
                }
            }

        }
        */
    }
}
