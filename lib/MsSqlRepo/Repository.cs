using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using TypeLib;
using System.Linq;

namespace MsSqlRepo
{
    public class Repository
    {
        string ConnectionString { get; }
        SqlConnection connection { get; }
        public Repository()
        {
            // Fetching from locally stored secrets
            string host = Helper.Globals.Get("host");
            string database = Helper.Globals.Get("db");
            string username = Helper.Globals.Get("usr");
            string password = Helper.Globals.Get("pwd");
            string port = Helper.Globals.Get("port");

            ConnectionString = $"Data Source={host};Initial Catalog={database};User Id={username};Password={password};";
            connection = new SqlConnection(ConnectionString);
            connection.Open();
        }
        /* ADLU => (Add, Delete, List, Update) 
         * 
         * In alphabetical order
         * ------
         * Article
         * Employee
         * Ingredient
         * Order
         * -------
         */
        public async Task<IEnumerable<Orders>> GetAllOrdersWithArticles()
        {
            IEnumerable<Orders> orders = await connection.QueryAsync<Orders>("GetAllOrders", commandType: CommandType.StoredProcedure);
            foreach (Orders order in orders)
            {
                order.Articles = (await connection.QueryAsync<Articles>("GetArticleOrders", new { OrderID = order.ID }, commandType: CommandType.StoredProcedure)).ToList();

            }
            return orders;

        }
        public async Task<int> AddArticle(Articles article)
        {
            string query = "INSERT INTO Articles(Name, BasePrice, Type) OUTPUT Inserted.Id VALUES(@Name, @BasePrice, @Type)";
            var result = (await connection.QueryAsync<Articles>(query, article)).ToList();
            return result.First().ID;
        }
        public async Task DeleteArticle(int id)
        {
            string query = "DELETE FROM Articles WHERE ID = @ID";
            var result = connection.QueryAsync(query, new { ID = id });
        }
        public async Task<List<Articles>> ListArticles()
        {
            string query = "SELECT * FROM Articles";
            var result = (await connection.QueryAsync<Articles>(query)).ToList();
            return result;
        }
        public async Task UpdateArticle(Articles article)
        {
            string query = "UPDATE Articles SET(Name = @Name, BasePrice = @BasePrice, Type = @Type)";
            var result = connection.QueryAsync<Articles>(query, article);
        }
        public async Task<Employees> AddEmployee(Employees employee)
        {
            string query = "INSERT INTO Employees(Name, LastName, Email, Password) VALUES(@Name, @LastName, @Email, @Password)";
            var result = await connection.QueryAsync<Employees>(query);
            return employee;
        }
        public async Task<bool> DeleteEmployee(int id)
        {
            string query = "DELETE FROM Employees WHERE ID = @ID";
            var result = connection.Execute(query, new { ID = id });
            return result > 0 ? true : false;
        }
        public async Task<List<Employees>> ListEmployees()
        {
            string query = "SELECT * FROM Employees";
            var result = connection.Query<Employees>(query);
            return result.ToList();
        }
        public async Task<bool> UpdateEmployee(Employees employee)
        {
            string query = "UPDATE Employees SET(Name = @Name, LastName = @LastName, Email = @Email, Password = @Password) WHERE ID = @ID";
            var result = await connection.QueryAsync<Employees>(query, employee);
            return result.GetType() == typeof(Employees) ? true : false;
        }
        public async Task<bool> AddIngredient(Ingredients ingredient)
        {
            string query = "INSERT INTO Ingredient(Name, Price) VALUES (@Name, @Price)";
            var result = await connection.QueryAsync<Ingredients>(query, ingredient);
            return result.GetType() == typeof(Ingredients) ? true : false;
        }
        public async Task DeleteIngredient(int id)
        {
            string query = "DELETE FROM Ingredients WHERE ID = @ID";
            var result = await connection.QueryAsync<Ingredients>(query, new { ID = id });
        }
        public async Task<List<Ingredients>> ListIngredients()
        {
            string query = "SELECT * FROM Ingredients";
            var result = await connection.QueryAsync<Ingredients>(query);
            return result.ToList();
        }
        public async Task UpdateIngredient(Ingredients ingredient)
        {
            string query = "UPDATE Ingredients SET(Name = @Name, Price = @Price)";
            var result = await connection.QueryAsync<Ingredients>(query, ingredient);
        }
        public async Task AddOrder(Orders order)
        {
            string query = "INSERT INTO Orders(Orderstatus, Price) VALUES (Orderstatus, Price)";
            var result = await connection.QueryAsync<Orders>(query, order);
        }
        public async Task DeleteOrder(int id)
        {
            string query = "DELETE FROM Orders WHERE ID = @ID";
            var result = await connection.QueryAsync<Orders>(query, new { ID = id });
        }
        public async Task<List<Orders>> ListOrders()
        {
            string query = "SELECT * FROM Orders";
            var result = await connection.QueryAsync<Orders>(query);
            return result.ToList();
        }
        public async Task UpdateOrder(Orders order)
        {
            string query = "UPDATE Orders SET(Orderstatus = @Orderstatus, Price = @price)";
            var result = await connection.QueryAsync<Orders>(query, order);
        }
        public async Task<IEnumerable<Articles>> GetAllArticlesWithIngredients()
        {
            IEnumerable<Articles> articles = await connection.QueryAsync<Articles>("getArticles", commandType: CommandType.StoredProcedure);
            foreach (Articles article in articles)
            {
                article.Ingredients = (await connection.QueryAsync<Ingredients>("getArticleIngredients", new { articleID = article.ID }, commandType: CommandType.StoredProcedure)).ToList();
            }
            return articles;
        }
    }
}
