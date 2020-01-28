using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeRepository
{
   public  class RepositoryEmployee
    {
        private string ConnectionString { get; }
        private SqlConnection connection { get; }
        public RepositoryEmployee()
        {
            ConnectionString = "Data Source=SQL6009.site4now.net;Initial Catalog=DB_A53DDD_JAKOB;User Id=DB_A53DDD_JAKOB_admin;Password=hunter12;";
            connection = new SqlConnection(ConnectionString);
        }
       public async Task<IEnumerable<Employees>> GetAllEmployees()
        {
            return (await connection.QueryAsync<Employees>("GetAllEmployees", commandType: CommandType.StoredProcedure)).ToList();
        }
       
    }
}
