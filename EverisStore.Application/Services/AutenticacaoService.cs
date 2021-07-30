using AutoMapper;
using EverisStore.Application.ViewModels;
using EverisStore.Domain.Models;
using EverisStore.Domain.Repositories;
using EverisStore.Infraestrutura;
using System.Threading.Tasks;

namespace EverisStore.Application.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        
        private readonly IUsuarioRepository _usuarioRepositorio;
        private readonly IMapper _mapper;

        public AutenticacaoService(IUsuarioRepository usuarioRepository,
                                   IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<string> Autenticar(AutenticarUsuarioViewModel autenticar)
        {
            var usuario = await _usuarioRepositorio.Consultar(autenticar.Email, autenticar.Senha);

            if (usuario == null)
                return "";

           return TokenGenerator.Gerar(usuario);

        }

        public async Task<bool> RegistrarUsuario(RegistrarUsuarioViewModel registrarUsuario)
        {
            var usuario = _mapper.Map<Usuario>(registrarUsuario);

            _usuarioRepositorio.Cadastrar(usuario);

            return await _usuarioRepositorio.UnitOfWork.Commit();
        }
    }
}
