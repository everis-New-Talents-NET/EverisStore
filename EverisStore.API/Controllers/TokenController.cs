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
        [HttpPost("login-sistema")]
        public IActionResult ResquestToken([FromQuery] UsuarioParams request)
        {
             if (request.Nome == "everis" && request.Senha == "dio")
             {
                 var claims = new []
                 {
                     new Claim(ClaimTypes.Name, request.Nome),
                    //  new Claims(ClaimTypes.Role, "admin")
                 };

                //código para armazenar a criptografia usada na criação do token
                 var key = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(_config["SecurityKey"])
                 );

                //recebe
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                 var token = new JwtSecurityToken(
                     issuer: "everistore.com.br",
                     audience: "everistore",
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(2),
                     signingCredentials: creds
                 );

                  return Ok( new {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

             }

             return NotFound();



        }


    }
}