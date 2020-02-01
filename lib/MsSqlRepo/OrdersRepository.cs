using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace MsSqlRepo
{
    public class OrdersRepository : GenericRepository<Orders>
    {
        string tableName;
        public OrdersRepository(string tableName) : base(tableName)
        {
            this.tableName = tableName;
        }
        public async Task MakeOrderAsync(Orders order, List<Articles> articles)
        {
            
            using (var connection = CreateConnection())
            {
                foreach (var article in articles)
                {
                    //var insertQuery = $"INSERT INTO {tableName} (ArticlesID, OrdersID) VALUES (@ArticlesID, @AOrderssID)";
                    //await connection.ExecuteAsync(insertQuery, new ArticleOrders { OrdersID = (int)order.ID, ArticlesID = (int)article.ID});
                    string sql = @"INSERT INTO  ArticleOrders (ArticlesID, OrdersID) VALUES (@ArticlesID, @OrdersID) SELECT CAST(SCOPE_IDENTITY() as int)";

                    var id = connection.Query<int>(sql, new { OrdersID = (int)order.ID, ArticlesID = (int)article.ID } ).Single();
                    var articleOrderRepo = new ArticleOrdersRepository("ArticleOrders");
                    article.ArticleOrder = (await articleOrderRepo.GetAsync(id));
                    var ingredientsRepo = new IngredientsRepository("Ingredients");
                    await ingredientsRepo.InsertCustomIngredientsAsync(article.ArticleOrder, article.Ingredients);

                }
            }

        }



    }
}
