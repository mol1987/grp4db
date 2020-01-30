using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLib
{
    public class Orders
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public int Orderstatus { get; set; }
        public float Price { get; set; }
        public int CustomerID { get; set; }
        public List<Articles> Articles { get; set; }
    }
}
