using API.Demo.PostgreSQL.Domains;
using API.Demo.PostgreSQL.Repositories.Interfaces;
using API.Demo.PostgreSQL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Demo.PostgreSQL.Controllers
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
        public async Task<IActionResult> AddProduct(ProductViewModel input)
        {
            try
            {
                Product p = new Product(input.Name, input.Price, input.Units);

                Product newProduct = await _productRepository.AddProduct(p);

                return StatusCode((int)HttpStatusCode.OK, newProduct);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Houve um erro interno ao adicionar o produto no banco de dados.");
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<Product> listProducts = _productRepository.GetAll();

                return StatusCode((int)HttpStatusCode.OK, listProducts);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Houve um erro interno ao retornar todos os produtos do banco de dados.");
            }
        }
    }
}
