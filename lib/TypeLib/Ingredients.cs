using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLib
{
    public class Ingredients : ICloneable
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
