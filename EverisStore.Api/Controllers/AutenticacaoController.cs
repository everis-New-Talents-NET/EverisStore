using EverisStore.Application.Services;
using EverisStore.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EverisStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {

        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Autenticar([FromBody] AutenticarUsuarioViewModel autenticarViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(lnq => lnq.Errors));

            var token = await _autenticacaoService.Autenticar(autenticarViewModel);

            return Ok(token);
           
        }
    }
}
