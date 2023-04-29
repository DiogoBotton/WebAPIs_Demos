using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.MongoDB.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Units { get; set; }

        public ProductModel(string name, double price, int units)
        {
            Name = name;
            Price = price;
            Units = units;
        }
    }
}
