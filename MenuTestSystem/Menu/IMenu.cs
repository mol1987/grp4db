using System.Collections.Generic;
using System.Threading.Tasks;

namespace MenuTestSystem.Menu
{
    public interface IMenu
    {
        List<IMenu> PagesList { get; set; }
        public string Name { get; set; }
        Task Print();
    }
}