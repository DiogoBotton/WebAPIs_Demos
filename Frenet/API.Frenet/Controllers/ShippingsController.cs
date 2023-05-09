using API.Frenet.Domains;
using API.Frenet.Models;
using API.Frenet.Repositories.Interfaces;
using API.Frenet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Frenet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingsController : ControllerBase
    {
        public IProductRepository _productRepository { get; set; }
        public IShippingRepository _shippingRepository { get; set; }
        public IQuoteRepository _quoteRepository { get; set; }

        public ShippingsController(IProductRepository productRepository, IShippingRepository shippingRepository, IQuoteRepository quoteRepository)
        {
            _productRepository = productRepository;
            _shippingRepository = shippingRepository;
            _quoteRepository = quoteRepository;
        }

        [HttpPost("quote")]
        public async Task<IActionResult> CalculateQuote(ProductModel input)
        {
            try
            {
                if (!input.ShippingItemArray.Any())
                    return StatusCode(400, "A lista de itens para envio [ShippingItemArray] não pode estar vazia.");

                // Create shipping in database
                Shipping shippingDb = new Shipping(input.SellerCEP, input.RecipientCEP, input.ShipmentInvoiceValue, input.ShippingServiceCode, input.RecipientCountry);
                shippingDb = await _shippingRepository.CreateAsync(shippingDb);

                // Save for reference shippingId on product and quote
                await _shippingRepository.UnitOfWork.SaveDbChanges();

                List<Product> newProducts = input.ShippingItemArray.Select(x => new Product
                {
                    Category = x.Category,
                    Height = x.Height,
                    Length = x.Length,
                    SKU = x.SKU,
                    Weight = x.Weight,
                    Width = x.Width,
                    ShippingId = shippingDb.Id
                }).ToList();

                // Request API Frenet
                QuoteResponseRequest quoteList = await FrenetApiService.QuoteProduct(input);

                if (!quoteList.ShippingSevicesArray.Any())
                    return StatusCode(400, "Houve um erro ao solicitar a cotação.");

                // Create new class quotes and order by best shipping price
                List<Quote> newQuotes = quoteList.ShippingSevicesArray.Select(x => new Quote 
                {
                    Carrier = x.Carrier,
                    CarrierCode = x.CarrierCode,
                    ServiceCode = x.ServiceCode,
                    DeliveryTime = x.DeliveryTime,
                    Msg = x.Msg,
                    OriginalDeliveryTime = x.OriginalDeliveryTime,
                    OriginalShippingPrice = Convert.ToDouble(x.OriginalShippingPrice),
                    ServiceDescription = x.ServiceDescription,
                    ShippingPrice = Convert.ToDouble(x.ShippingPrice),
                    ShippingId = shippingDb.Id
                }).OrderBy(x => x.ShippingPrice).ToList();

                // Create products and quotes in database
                await _productRepository.AddRangeAsync(newProducts);
                await _quoteRepository.AddRangeAsync(newQuotes);

                // Save all changes in database
                await _shippingRepository.UnitOfWork.SaveDbChanges();

                return Ok(newQuotes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno ao calcular cotação do frete do produto.");
            }
        }

        [HttpGet("quote/{cep}")]
        public async Task<IActionResult> GetQuoteByCep(string cep)
        {
            try
            {
                var shippingsDb = await _shippingRepository.GetAllByCEP(cep);

                // Returns all shippings by cep and bind the best shipping price
                var result = shippingsDb.Select(async x =>new
                {
                    x.SellerCEP,
                    x.RecipientCEP,
                    x.ShipmentInvoiceValue,
                    x.ShippingServiceCode,
                    BestShippingPrice = (await _quoteRepository.GetAllQuotesByShippingId(x.Id)).OrderBy(x => x.ShippingPrice).Select(x => new
                    {
                        x.ShippingPrice,
                        x.OriginalShippingPrice,
                        x.ServiceCode,
                        x.ServiceDescription,
                        x.Carrier,
                        x.CarrierCode,
                        x.DeliveryTime,
                        x.OriginalDeliveryTime,
                        x.Msg,
                    }).FirstOrDefault()
                });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno ao calcular cotação do frete do produto.");
            }
        }
    }
}
