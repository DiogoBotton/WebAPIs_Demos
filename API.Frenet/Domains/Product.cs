using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Domains
{
    public class Product : AbstractDomain
    {
        public double Height { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public double Width { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public string SellerCEP { get; set; }
        public string RecipientCEP { get; set; }
        public int QuoteId { get; set; }
    }
}