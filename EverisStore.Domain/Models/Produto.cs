using EverisStore.Domain.DomainObjects;
using System;
using System.Collections.Generic;
using Bogus;

namespace EverisStore.Domain.Models
{
    public class Produto : Entity
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadeEstoque { get; set; }
        public Dimensoes Dimensoes { get; set; }
        public Categoria Categoria { get; set; }

        public Produto() {}
        
        public Produto(string nome, string descricao, bool ativo, decimal valor, int categoriaId, DateTime dataCadastro, string imagem, Dimensoes dimensoes)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = dataCadastro;
            Dimensoes = dimensoes;
        }

        public List<Produto> ObterTodosProdutos()
        {
            try
            {
                var indexCategoria = 123456;

                var categorias = new Faker<Categoria>("pt_BR")
                    .RuleFor(p => p.Id, p => indexCategoria++)
                    .RuleFor(p => p.Nome, p => p.Company.Random.Word())
                    .RuleFor(p => p.Codigo, p => p.Random.Int(1, 100));
                    
                var indexProduto = 123456;
                var produtos = new Faker<Produto>("pt_BR")
                    .RuleFor(p => p.Id, p => indexProduto++)
                    .RuleFor(p => p.DataCadastro, p => new DateTime())
                    .RuleFor(p => p.Nome, p => p.Commerce.Department())
                    .RuleFor(p => p.Descricao, p => p.Commerce.Department())
                    .RuleFor(p => p.Valor, p => p.Random.Decimal(10, 1200))
                    .RuleFor(p => p.QuantidadeEstoque, p => p.Random.Int(1, 12))
                    .RuleFor(p => p.Categoria, p => categorias.Generate() )
                    .Generate(10);

                return produtos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
         
        }


        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarCategoria(Categoria categoria)
        {
            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public void AlterarDescricao(string descricao)
        {
           
            Descricao = descricao;
        }       

        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }

        public bool PossuiEstoque(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }
        
        
    }

    public class ProdutoParams
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string NomeCategoria { get; set; }
    }
}
