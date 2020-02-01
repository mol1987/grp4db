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
    public class ArticlesRepository : GenericRepository<Articles>
    {
        private readonly string _tableName;
        public ArticlesRepository(string tableName) : base(tableName)
        {
            _tableName = tableName;
        }

        new public async Task<IEnumerable<Articles>> GetAllAsync()
        {
            using (var connection = base.CreateConnection())
            {
                IEnumerable<Articles> articles = await connection.QueryAsync<Articles>("getArticles", commandType: CommandType.StoredProcedure);
                foreach (Articles article in articles)
                {
                    article.Ingredients = (await connection.QueryAsync<Ingredients>("getArticleIngredients", new { articleID = article.ID }, commandType: CommandType.StoredProcedure)).ToList();
                }
                return articles;
            }
        }
        new public async Task<Articles> GetAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Articles>($"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");
                result.Ingredients = (await connection.QueryAsync<Ingredients>("getArticleIngredients", new { articleID = result.ID }, commandType: CommandType.StoredProcedure)).ToList();
                return result;
            }
        }


    }
}
