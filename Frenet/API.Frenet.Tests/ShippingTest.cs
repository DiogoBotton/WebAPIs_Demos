using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace API.Frenet.Tests
{
    public class ShippingTest
    {
        [Fact]
        public async void AddCalculateQuotes_SUCCESS()
        {
            // Act
            Uri baseAddress = new Uri("http://localhost:5000/api/shippings/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {

                using (var content = new StringContent("{  \"SellerCEP\": \"04757020\",  \"RecipientCEP\": \"14270000\",  \"ShipmentInvoiceValue\": 320.685,  \"ShippingServiceCode\": null,  \"ShippingItemArray\": [    {      \"Height\": 2,      \"Length\": 33,      \"Quantity\": 1,      \"Weight\": 1.18,      \"Width\": 47,      \"SKU\": \"IDW_54626\",      \"Category\": \"Running\"    },    {      \"Height\": 5,      \"Length\": 15,      \"Quantity\": 1,      \"Weight\": 0.5,      \"Width\": 29    }  ],  \"RecipientCountry\": \"BR\"}", System.Text.Encoding.Default, "application/json"))
                {
                    using (var response = await httpClient.PostAsync("quote", content))
                    {
                        // Assert
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    }
                }
            }
        }

        [Theory]
        [InlineData("09950110")]
        public async void GetQuotes_SUCCESS(string cep)
        {
            // Act
            Uri baseAddress = new Uri("http://localhost:5000/api/shippings/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var response = await httpClient.GetAsync($"quote/{cep}"))
                {
                    // Assert
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }
    }
}
