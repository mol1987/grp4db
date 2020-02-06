using System;
using System.Threading.Tasks;

namespace EmployeeRepository
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // calling the repository class
            RepositoryEmployee repo1 = new RepositoryEmployee();
            foreach ( Employees employee in await repo1.GetAllEmployees())
            {
                Console.WriteLine("\n Id" + employee.ID);
                Console.WriteLine("Name:" +employee.Name);
                Console.WriteLine("LastName:"  +employee.LastName);
                Console.WriteLine(" Email:" + employee.Email);
                Console.WriteLine("Password:" +employee.Password);
            }
            Console.ReadLine();
        }
    }
}
