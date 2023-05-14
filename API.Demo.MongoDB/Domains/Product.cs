using API.Demo.MongoDB.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.MongoDB.Domains
{
    public class Product : AbstractDomain
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Units { get; set; }

        public Product(string name, double price, int units)
        {
            Name = name;
            Price = price;
            Units = units;
        }

        public void Update(string name, double price, int units)
        {
            Name = name;
            Price = price;
            Units = units;
        }
    }
}
