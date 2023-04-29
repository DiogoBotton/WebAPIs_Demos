using API.Demo.MongoDB.Domains;
using API.Demo.MongoDB.Models;
using API.Demo.MongoDB.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Demo.MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("new")]
        public async Task<IActionResult> Insert([FromBody] ProductModel input)
        {
            try
            {
                Product newProduct = new Product(input.Name, input.Price, input.Units);
                await _productRepository.CreateAsync(newProduct);

                return Ok(newProduct);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno ao inserir novo produto no banco de dados.");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _productRepository.GetAllAsync());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno ao resgatar todos os produtos no banco de dados.");
            }
        }
    }
}
