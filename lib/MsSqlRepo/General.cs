using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace MsSqlRepo
{
    public static class General
    {
        public static ArticlesRepository articlesRepo = new ArticlesRepository("Articles");
        public static OrdersRepository ordersRepo = new OrdersRepository("Orders");
        public static IngredientsRepository ingredientsRepo = new IngredientsRepository("Ingredients");
        public static EmployeesRepository employeesRepo = new EmployeesRepository("Employees");
        public static async Task<List<Articles>> getArticlesFromOrder(this List<Articles> a, Orders o)
        {
            return (await articlesRepo.GetAllAsync(o)).ToList();
        }
        public static async Task<List<Articles>> getArticles(this List<Articles> a)
        {
            return (await articlesRepo.GetAllAsync()).ToList();
        }
        public static async Task<Articles> getArticle(this Articles a, int id)
        {
            return (await articlesRepo.GetAsync(id));
        }

        public static async Task insertOrder(this Orders o)
        {
            await ordersRepo.InsertAsync(o);
        }
    }
}
