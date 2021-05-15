using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DotNetCore3.Domains;
using WebApi.DotNetCore3.Inputs;
using WebApi.DotNetCore3.Interfaces;

namespace WebApi.DotNetCore3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        public IProdutoRepository _produtoRepository { get; set; }

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateProduto(ProdutoInput input)
        {
            try
            {
                var produtoDb = await _produtoRepository.Create(new Produto(input.Nome, input.Fabricante, input.ValorUnidade, input.QtdEstoque));

                return StatusCode(200, produtoDb);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new List<string>()
                {
                    "Ocorreu algum erro no cadastro de um produto.",
                     ex.Message
                });
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllProdutos()
        {
            try
            {
                var produtosDb = _produtoRepository.GetAllProdutos();

                return StatusCode((int)HttpStatusCode.OK, produtosDb);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new List<string>()
                {
                    "Ocorreu algum erro no resgate de todos os produtos.",
                     ex.Message
                });
            }
        }
    }
}
