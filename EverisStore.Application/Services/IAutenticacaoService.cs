using EverisStore.Application.ViewModels;
using System.Threading.Tasks;

namespace EverisStore.Application.Services
{
    public interface IAutenticacaoService
    {
        Task<bool> RegistrarUsuario(RegistrarUsuarioViewModel registrarUsuario);
        Task<string> Autenticar(AutenticarUsuarioViewModel autenticar);
    }
}
