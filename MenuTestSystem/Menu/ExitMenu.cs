using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MenuTestSystem.Menu
{
    public class ExitMenu : IMenu
    {
        public List<IMenu> PagesList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get; set; }
        public ExitMenu()
        {
            Name = "ExitMenu";
        }
        public async Task Print()
        {
            Console.WriteLine("bye bye");
        }
    }
}
