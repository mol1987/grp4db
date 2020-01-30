using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace RepositoryOrder
{
    public class OrderRepository
    {
        private string ConnectionString { get; }
        private SqlConnection connection { get; }
        public OrderRepository()
        {
            ConnectionString = "Data Source=SQL6009.site4now.net;Initial Catalog=DB_A53DDD_JAKOB;User Id=DB_A53DDD_JAKOB_admin;Password=hunter12;";
            connection = new SqlConnection(ConnectionString);
            connection.Open();
        }
        public async Task<IEnumerable<Orders>> GetAllOrdersWithArticles()
        {
            IEnumerable<Orders> orders = await connection.QueryAsync<Orders>("GetAllOrders", commandType: CommandType.StoredProcedure);
            foreach (Orders order in orders)
            {
                order.Articles = (await connection.QueryAsync<Articles>("GetArticleOrders", new { OrderID = order.ID }, commandType: CommandType.StoredProcedure)).ToList();

            }
            return orders;

        }
    }
}
