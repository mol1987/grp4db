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

        public async Task InsertAsync(Orders order)
        {
            string insertQuery = base.GenerateInsertQuery(order);

            insertQuery += " SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = CreateConnection())
            {
                var id = (await connection.QueryAsync<int>(insertQuery, order)).Single();
                order.ID = id;
            }
            
        }

        public async Task MakeOrderAsync(Orders order, List<Articles> articles)
        {
            using (var connection = CreateConnection())
            {
                foreach (var article in articles)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        string sql = @"INSERT INTO  ArticleOrders (ArticlesID, OrdersID) VALUES (@ArticlesID, @OrdersID) SELECT CAST(SCOPE_IDENTITY() as int)";

                        var id = (await connection.QueryAsync<int>(sql, new { OrdersID = (int)order.ID, ArticlesID = (int)article.ID }, transaction: transaction)).Single();

                        article.ArticleOrder = (await connection.QuerySingleOrDefaultAsync<ArticleOrders>("Select * from ArticleOrders where ID = @ArticleOrdersID", new { ArticleOrdersID = id }, transaction: transaction));

                        foreach (var ingredient in article.Ingredients)
                        {
                            var insertQuery = $"INSERT INTO ArticleOrdersIngredients (IngredientsID, ArticleOrdersID) VALUES (@IngredientsID, @ArticleOrdersID)";
                            await connection.ExecuteAsync(insertQuery, new ArticleOrdersIngredients { IngredientsID = (int)ingredient.ID, ArticleOrdersID = article.ArticleOrder.ID }, transaction: transaction);
                        }

                        transaction.Commit();
                    }
        
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
