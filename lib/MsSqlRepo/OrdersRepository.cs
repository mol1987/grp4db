using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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

                    var id = (await connection.QueryAsync<int>(sql, new { OrdersID = (int)order.ID, ArticlesID = (int)article.ID } )).Single();

                    article.ArticleOrder = (await connection.QuerySingleOrDefaultAsync<ArticleOrders>("Select * from ArticleOrders where ID = @ArticleOrdersID", new { ArticleOrdersID = id }));

                    var ingredientsRepo = new IngredientsRepository("Ingredients");
                    await ingredientsRepo.InsertCustomIngredientsAsync(article.ArticleOrder, article.Ingredients);

                }
            }

        }

        new public async Task<Articles> GetAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Articles>($"SELECT * FROM {tableName} WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{tableName} with id [{id}] could not be found.");
                result.Ingredients = (await connection.QueryAsync<Ingredients>("getArticleIngredients", new { articleID = result.ID }, commandType: CommandType.StoredProcedure)).ToList();
                return result;
            }
        }

    }
}
