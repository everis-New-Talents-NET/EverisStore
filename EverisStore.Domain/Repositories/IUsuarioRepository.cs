using EverisStore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace EverisStore.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public void Cadastrar(Usuario usuario);
        public Task<Usuario> Consultar(string email, string senha);
    }
}
