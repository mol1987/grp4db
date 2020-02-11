using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeställningsTerminal.Menu
{
    public interface IMenu
    {
        List<IMenu> PagesList { get; set; }
        public string Name { get; set; }
        Task Print();
    }
}