using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace MsSqlRepo
{
    public class ArticleOrdersRepository : GenericRepository<ArticleOrders>
    {
        string tableName;
        public ArticleOrdersRepository(string tableName) : base(tableName)
        {
            this.tableName = tableName;
        }

    }
}
