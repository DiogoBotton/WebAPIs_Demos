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
    public class ProductsController : ControllerBase
    {
        public IProductRepository _productRepository { get; set; }

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("quote")]
        public async Task<IActionResult> CalculateQuote(ProductModel input)
        {
            try
            {
                var quote = await FrenetApiService.QuoteProduct(input);
                return Ok(quote);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno ao calcular cotação do frete do produto.");
            }
        }

        [HttpGet("quote/{cep}")]
        public async Task<IActionResult> GetQuoteByCep(string cep)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno ao calcular cotação do frete do produto.");
            }
        }
    }
}
