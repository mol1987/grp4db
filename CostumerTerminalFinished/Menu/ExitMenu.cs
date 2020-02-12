using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeställningsTerminal.Menu
{
    public class ExitMenu : IMenu
    {
        public List<IMenu> PagesList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get; set; }
        public ExitMenu()
        {
            Name = "Avsluta";
        }
        public async Task Print()
        {
            Console.WriteLine("Hejdå, välkommen åter");
        }
    }
}
