using API.Demo.MongoDB.Domains;
using API.Demo.MongoDB.Models;
using API.Demo.MongoDB.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] ProductModel input)
        {
            try
            {
                var productDb = await _productRepository.GetById(id);

                if (productDb != null)
                {
                    productDb.Update(input.Name, input.Price, input.Units);
                    await _productRepository.UpdateAsync(id, productDb);

                    return Ok("Produto atualizado com sucesso.");
                }
                else
                    return StatusCode((int)HttpStatusCode.NoContent, "Não foi encontrado produto com o id específico.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno ao atualizar o produto específico no banco de dados.");
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                if (id.Length < 24 || id.Length > 24)
                    return StatusCode(400, "Id não pode ser menor ou maior que 24.");

                var result = await _productRepository.GetById(id);

                if (result != null)
                    return Ok(result);
                else
                    return StatusCode((int)HttpStatusCode.NoContent, "Não foi encontrado produto com o id específico.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro interno ao resgatar o produto específico no banco de dados.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            try
            {
                var productDb = await _productRepository.GetById(id);

                if (productDb != null)
                {
                    await _productRepository.DeleteAsync(id);
                    return Ok();
                }
                else
                    return StatusCode((int)HttpStatusCode.NoContent, "Não foi encontrado produto com o id específico.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro interno ao deletar o produto específico no banco de dados.");
            }
        }
    }
}
