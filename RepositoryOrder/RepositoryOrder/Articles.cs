using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryOrder
{
    public class Articles
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float BasePrice { get; set; }
        public string Type { get; set; }
        public List<Ingredients> Ingredients { get; set; }
    }
}

