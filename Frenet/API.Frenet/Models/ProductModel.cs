using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Models
{
    public class ProductModel
    {
        public string SellerCEP { get; set; }
        public string RecipientCEP { get; set; }
        public double ShipmentInvoiceValue { get; set; }
        public string ShippingServiceCode { get; set; }
        public Shippingitemarray[] ShippingItemArray { get; set; }
        public string RecipientCountry { get; set; }

        public class Shippingitemarray
        {
            public double Height { get; set; }
            public double Length { get; set; }
            public double Quantity { get; set; }
            public double Weight { get; set; }
            public double Width { get; set; }
            public string SKU { get; set; }
            public string Category { get; set; }
        }
    }
}
