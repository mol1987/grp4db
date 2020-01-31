using System;
using System.Collections.Generic;
using System.Text;
using TypeLib;

namespace MsSqlRepo
{
    public class IngredientsRepository : GenericRepository<Ingredients>
    {
        public IngredientsRepository(string tableName) : base(tableName)
        {
           
        }
    }
}
