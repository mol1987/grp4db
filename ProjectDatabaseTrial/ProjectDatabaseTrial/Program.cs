using System;

namespace ProjectDatabaseTrial
{
    class Program
    {
        static void Main(string[] args)
        {// brings List of Articles from database
            ArticklesRepository repo = new ArticklesRepository();
            foreach ( Artickles artickle in repo.artickles())
            {
                Console.WriteLine(artickle.Type);
                Console.WriteLine(artickle.ArticklesId);
                Console.WriteLine(artickle.Name);
                Console.WriteLine(artickle.BasePrice);
                
                Console.WriteLine("----------------------------");
            }
        }
    }
}
