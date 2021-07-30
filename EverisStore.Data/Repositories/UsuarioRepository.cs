using EverisStore.Domain.Models;
using EverisStore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EverisStore.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EverisStoreContext _context;

        public UsuarioRepository(EverisStoreContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Cadastrar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public async Task<Usuario> Consultar(string email, string senha)
        {
            return await _context.Usuarios.Include(lnq => lnq.Perfil).AsNoTracking().FirstOrDefaultAsync(lnq => lnq.Email == email && lnq.Senha == senha);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
