using API.Frenet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Frenet.Services
{
    public class FrenetApiService
    {
        private static Uri _baseAddress => new Uri("https://private-anon-63bf350c0b-frenetapi.apiary-mock.com/");
        private const string TOKEN = "30EBE94BRD919R46AFRAD23R5D99E909C6C0";

        public static async Task<QuoteResponseRequest> QuoteProduct(ProductModel input)
        {
            using (var httpClient = new HttpClient { BaseAddress = _baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("token", TOKEN);

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
