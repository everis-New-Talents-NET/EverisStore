using System;
using EverisStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EverisStore.API.Controllers
{
   
    public class ProdutosController : BaseController
    {
        // GET
        [HttpGet("obter-todos")]
        public IActionResult ObterTodosProdutos() => throw new ArgumentException("Show error...");
        
        [HttpPost("salvar-produtos")]
        public IActionResult SalvarProduto([FromBody] Produto request) => Ok(request);
        
        [HttpGet("obter-por-id/{id:length(6)}")]
        public IActionResult ObterProdutoById(string id) => Ok(id);
    }

}