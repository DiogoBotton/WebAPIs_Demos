using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Domains
{
    public class Shipping : AbstractDomain
    {
        public string SellerCEP { get; set; }
        public string RecipientCEP { get; set; }
        public double ShipmentInvoiceValue { get; set; }
        public string ShippingServiceCode { get; set; }
        public string RecipientCountry { get; set; }

        public Shipping(string sellerCEP, string recipientCEP, double shipmentInvoiceValue, string shippingServiceCode, string recipientCountry)
        {
            SellerCEP = sellerCEP;
            RecipientCEP = recipientCEP;
            ShipmentInvoiceValue = shipmentInvoiceValue;
            ShippingServiceCode = shippingServiceCode;
            RecipientCountry = recipientCountry;
        }
    }
}
