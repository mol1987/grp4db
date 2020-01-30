using System;
using System.Threading.Tasks;

namespace RepositoryOrder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            OrderRepository repo = new OrderRepository();
            foreach (Orders order in (await repo.GetAllOrdersWithArticles()))
            {
                Console.WriteLine(order.ID);
                Console.WriteLine(order.OrderStatus);
                Console.WriteLine(order.TimeCreated);
                foreach( Articles articleOrderItem in order.Articles)
                {
                    Console.Write( " " + articleOrderItem.Name.Trim(' ') +",");
                }
            }
        }
    }
}
