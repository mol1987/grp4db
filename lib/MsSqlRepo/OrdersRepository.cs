using System;
using System.Collections.Generic;
using System.Text;
using TypeLib;

namespace MsSqlRepo
{
    public class OrdersRepository : GenericRepository<Orders>
    {
        public OrdersRepository(string tableName) : base(tableName)
        {
           
        }
    }
}
