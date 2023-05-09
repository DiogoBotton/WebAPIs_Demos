using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Domains
{
    public class Quote : AbstractDomain
    {
        public string Carrier { get; set; }
        public string CarrierCode { get; set; }
        public string DeliveryTime { get; set; }
        public string Msg { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceDescription { get; set; }
        public double ShippingPrice { get; set; }
        public string OriginalDeliveryTime { get; set; }
        public double OriginalShippingPrice { get; set; }
        public int ShippingId { get; set; }
    }
}