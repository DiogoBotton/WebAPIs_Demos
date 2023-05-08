using API.Frenet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Frenet.Services
{
    public static class FrenetApiService
    {
        public static async Task<QuoteResponseRequest> QuoteProduct(ProductModel input)
        {
            Uri baseAddress = new Uri("https://private-anon-63bf350c0b-frenetapi.apiary-mock.com/");
            string token = "30EBE94BRD919R46AFRAD23R5D99E909C6C0";

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("token", token);

                using (var content = new StringContent(JsonConvert.SerializeObject(input), System.Text.Encoding.Default, "application/json"))
                {
                    using (var response = await httpClient.PostAsync("shipping/quote", content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        QuoteResponseRequest result = JsonConvert.DeserializeObject<QuoteResponseRequest>(responseData);

                        return result;
                    }
                }
            }
        }
    }
}
