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
        /// <summary>
        /// Inserts an order with all the articles and custom ingredients contained in order.
        /// gets the orderid and stores it in order
        /// </summary>
        /// <param name="order">Created order object</param>
        /// <returns></returns>
        public async Task InsertAsync(Orders order)
        {
            var orderTemp = new  Orders { CustomerID = order.CustomerID, Price = order.Price };
            string insertQuery = base.GenerateInsertQuery(orderTemp);
            insertQuery += " SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = CreateConnection())
            {
                order.ID = (await connection.QueryAsync<int>(insertQuery, orderTemp)).Single();
                foreach (var article in order.Articles)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        string sql = @"INSERT INTO ArticleOrders (ArticlesID, OrdersID) VALUES (@ArticlesID, @OrdersID) SELECT CAST(SCOPE_IDENTITY() as int)";
                        var id = (await connection.QueryAsync<int>(sql, new { OrdersID = (int)order.ID, ArticlesID = (int)article.ID }, transaction: transaction)).Single();
                        article.ArticleOrder = (await connection.QuerySingleOrDefaultAsync<ArticleOrders>("Select * from ArticleOrders where ID = @ArticleOrdersID", new { ArticleOrdersID = id }, transaction: transaction));

                        foreach (var ingredient in article.Ingredients)
                        {
                            var sqlQuery = $"INSERT INTO ArticleOrdersIngredients (IngredientsID, ArticleOrdersID) VALUES (@IngredientsID, @ArticleOrdersID)";
                            await connection.ExecuteAsync(sqlQuery, new ArticleOrdersIngredients { IngredientsID = (int)ingredient.ID, ArticleOrdersID = article.ArticleOrder.ID }, transaction: transaction);
                        }
                        transaction.Commit();
                    }
                }
            }
        } 
    }
}
