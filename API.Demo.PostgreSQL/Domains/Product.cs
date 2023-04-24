using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.PostgreSQL.Domains
{
    // Nomes de tabelas devem ser em letras minusculas no PostgreSQL
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("units")]
        public int Units { get; set; }

        public Product(string name, double price, int units)
        {
            Name = name;
            Price = price;
            Units = units;
        }
    }
}
