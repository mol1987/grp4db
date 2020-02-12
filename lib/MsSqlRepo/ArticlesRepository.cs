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

        new public async Task<IEnumerable<Articles>> GetAllAsync(Orders order)
        {
            using (var connection = base.CreateConnection())
            {
                IEnumerable<ArticleOrders> articleOrders = await connection.QueryAsync<ArticleOrders>($"SELECT * FROM ArticleOrders WHERE OrdersID=@Id", new { Id = order.ID });
                List<Articles> articles = new List<Articles>();

                string sql = $"select Ingredients.* FROM ARTICLES INNER JOIN ArticleOrders ON Articles.ID = ArticleOrders.ArticlesID inner join ArticleOrdersIngredients on ArticleOrdersIngredients.ArticleOrdersID = ArticleOrders.ID inner join Ingredients on Ingredients.ID = ArticleOrdersIngredients.IngredientsID WHERE ArticleOrders.OrdersID = @OrdersID and ArticleOrders.ID = @ArticleOrdersID";

                foreach (ArticleOrders articleOrdersItem in articleOrders)
                {
                    Articles article = await connection.QuerySingleOrDefaultAsync<Articles>($"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = articleOrdersItem.ArticlesID });
                    article.Ingredients = new List<Ingredients>();
                    article.Ingredients = (await connection.QueryAsync<Ingredients>(sql, new { OrdersID = order.ID, ArticleOrdersID = articleOrdersItem.ID })).ToList();
                    articles.Add(article);
                }

                //string sql = $"select ArticleOrdersIngredients.* FROM ARTICLES INNER JOIN ArticleOrders ON Articles.ID = ArticleOrders.ArticlesID inner join ArticleOrdersIngredients on ArticleOrdersIngredients.ArticleOrdersID = ArticleOrders.ID WHERE ArticleOrders.OrdersID = @OrdersID and ArticleOrders.ID = @ArticleOrdersID";


                return (IEnumerable<Articles>)articles;
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
