﻿using Dapper;
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
        public async Task UpdateAsync(Articles article)
        {
            using (var connection = CreateConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    string sql = $"Delete from ArticleIngredients WHERE ArticleID = {article.ID }";
                    await connection.ExecuteAsync(sql, transaction: transaction);
                    
                    foreach (var ingredient in article.Ingredients)
                    {
                        var sqlQuery = $"INSERT INTO ArticleIngredients (ArticleID, IngredientID) VALUES (@ArticleID, @IngredientID)";
                        await connection.ExecuteAsync(sqlQuery, new { ArticleID = article.ID, IngredientID = ingredient.ID }, transaction: transaction);
                    }
                    transaction.Commit();
                }
            }
        }
        public async Task InsertAsync(Articles article)
        {
            using (var connection = CreateConnection())
            {

                using (var transaction = connection.BeginTransaction())
                {
                    string sql = @"INSERT INTO Articles (Name, BasePrice, Type) VALUES (@Name, @BasePrice, @Type) SELECT CAST(SCOPE_IDENTITY() as int)";
                    var id = (await connection.QueryAsync<int>(sql, new { Name = article.Name, BasePrice = article.BasePrice, Type = article.Type }, transaction: transaction)).Single();
                    article.ID = id;
                    foreach (var ingredient in article.Ingredients)
                    {
                        var sqlQuery = $"INSERT INTO ArticleIngredients (ArticleID, IngredientID) VALUES (@ArticleID, @IngredientID)";
                        await connection.ExecuteAsync(sqlQuery, new { ArticleID = article.ID, IngredientID = ingredient.ID }, transaction: transaction);
                    }
                    transaction.Commit();
                }
            }
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
