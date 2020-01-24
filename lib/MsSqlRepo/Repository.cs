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
