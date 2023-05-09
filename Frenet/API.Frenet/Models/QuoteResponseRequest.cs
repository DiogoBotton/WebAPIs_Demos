using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Frenet.Models
{
    public class QuoteResponseRequest
    {
        public Shippingsevicesarray[] ShippingSevicesArray { get; set; }
        public int Timeout { get; set; }
        public class Shippingsevicesarray
        {
            public string Carrier { get; set; }
            public string CarrierCode { get; set; }
            public string DeliveryTime { get; set; }
            public string Msg { get; set; }
            public string ServiceCode { get; set; }
            public string ServiceDescription { get; set; }
            public string ShippingPrice { get; set; }
            public string OriginalDeliveryTime { get; set; }
            public string OriginalShippingPrice { get; set; }
            public bool Error { get; set; }
        }
    }
}
