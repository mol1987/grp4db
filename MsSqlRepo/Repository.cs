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
            ConnectionString = "Data Source=sql6009.site4now.net;Initial Catalog=DB_A53DDD_JAKOB;User Id=DB_A53DDD_JAKOB_admin;Password=hunter12;";
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
