using System.Text; //Encoding.UTF8
using System.Security.AccessControl;
using System.Net.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using EverisStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; //Configuratio
using System.Security.Claims; //Para claims
using Microsoft.IdentityModel.Tokens; //Para SymmetricSecurityKey
using System.IdentityModel.Tokens.Jwt; //Para SymmetricSecurityKey
using Microsoft.AspNetCore.Authorization; //Para SymmetricSecurityKey

namespace EverisStore.API.Controllers
{
    public class TokenController : BaseController
    {
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("obter-token")]
        public IActionResult RequestToken([FromBody] UsuarioParams request)
        {
            if (request.Nome == "everis" && request.Senha == "dio")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                //código para armazenar a criptografia usada na criação do token
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["SecurityKey"])
                );

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, request.Nome),
                        //  new Claims(ClaimTypes.Role, "admin")
                    }),

                    SigningCredentials = new SigningCredentials(key,
                            SecurityAlgorithms.HmacSha256Signature),
                    Issuer = "everisstore.com.br",
                    Audience = "everisstore",
                    Expires = DateTime.UtcNow.AddSeconds(20),

                };

              var token =  tokenHandler.CreateToken(tokenDescriptor);
                return Ok(tokenHandler.WriteToken(token));
            }

            return NotFound();
        }


        [HttpPost("obter-usuario-autenticado")]
        public string Authenticated() => $"Autenticado - {User.Identity.Name}";
    }
}