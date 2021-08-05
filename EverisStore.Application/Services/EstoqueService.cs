using EverisStore.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace EverisStore.Application.Services
{
    public class EstoqueService : IEstoqueService
    {
       
      
        public async Task<string> DebitarEstoque(Guid produtoId, int quantidade)
        {
            return await Task.Run(() => $"Debitando Estoque do Produto {produtoId} Qtd: {quantidade}").ConfigureAwait(false);
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var sucesso = await ReporItemEstoque(produtoId, quantidade);

            if (!sucesso) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return true;
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}