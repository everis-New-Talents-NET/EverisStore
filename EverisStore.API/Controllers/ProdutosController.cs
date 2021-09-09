using System;
using System.Collections.Generic;
using System.Linq;
using EverisStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EverisStore.API.Controllers
{
    public class ProdutosController : BaseController
    {
        public static List<Produto> _produtos = new List<Produto>();

        public ProdutosController()
        {
            if (_produtos.Count == 0)
                _produtos = new Produto().ObterTodosProdutos();
        }

        // GET
        [HttpGet("obter-todos")]
        public IActionResult ObterTodosProdutos()
        {
            _produtos = new Produto().ObterTodosProdutos();
            return Ok(_produtos);
        }


        [HttpGet("obter-produto-por-nome/{nomeProduto}")]
        public IActionResult ObterProdutoByNome(string nomeProduto) =>
            Ok(_produtos.FirstOrDefault(p => nomeProduto.Equals(p.Nome)));


        [HttpGet("obter-por-id/{id:length(6)}")]
        public IActionResult ObterProdutoById(int id) => Ok(_produtos.FirstOrDefault(p => id.Equals(p.Id)));

        [HttpGet("obter-produto-por-descricao")]
        public IActionResult ObterProdutoByDescricao([FromQuery] ProdutoParams request)
        {
            var produtos = _produtos.FirstOrDefault(p => request.Descricao.Equals(p.Descricao));
            return Ok(produtos);
        }

        [HttpPost("obter-produto-por-categoria")]
        public IActionResult ObterProdutoByCategoriaNome([FromBody] ProdutoParams request)
        {
            var produtos = _produtos.FirstOrDefault(p => request.NomeCategoria.Equals(p.Categoria?.Nome));
            return Ok(produtos);
        }


        [HttpPost("salvar-produtos")]
        public IActionResult SalvarProduto([FromBody] Produto request)
        {
            var result = new
            {
                Mensagem = $"Produto {request.Id} foi gravado com sucesso!",
                Data = request,
                Acoes = new List<string>
                {
                    {$"{Url.Action("ObterTodosProdutos",  values: null)}"}
                },
            };

            return Ok(result);
        }

    }
}