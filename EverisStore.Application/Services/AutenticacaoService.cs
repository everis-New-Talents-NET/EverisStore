using EverisStore.Application.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EverisStore.Application.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AutenticacaoService(SignInManager<IdentityUser> signInManager,
                                   UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<string>> RegistrarUsuario(RegistrarUsuarioViewModel registrarUsuario)
        {
            var usuario = new IdentityUser
            {
                UserName = registrarUsuario.Email,
                Email = registrarUsuario.Email,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(usuario, registrarUsuario.Senha);

            if (!resultado.Succeeded)
                return resultado.Errors.Select(e => e.Description);

            await _signInManager.SignInAsync(usuario, false);

            return Enumerable.Empty<string>();
        }
    }
}
