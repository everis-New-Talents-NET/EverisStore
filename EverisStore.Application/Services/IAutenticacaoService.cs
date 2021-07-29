using EverisStore.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EverisStore.Application.Services
{
    public interface IAutenticacaoService
    {
        Task<IEnumerable<string>> RegistrarUsuario(RegistrarUsuarioViewModel registrarUsuario);
    }
}
