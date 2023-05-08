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
        public float ShipmentInvoiceValue { get; set; }
        public object ShippingServiceCode { get; set; }
        public Shippingitemarray[] ShippingItemArray { get; set; }
        public string RecipientCountry { get; set; }

        public class Shippingitemarray
        {
            public int Height { get; set; }
            public int Length { get; set; }
            public int Quantity { get; set; }
            public float Weight { get; set; }
            public int Width { get; set; }
            public string SKU { get; set; }
            public string Category { get; set; }
        }
    }
}
