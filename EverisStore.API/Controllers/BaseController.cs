using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverisStore.API.Controllers
{
    //https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authorization/limitingidentitybyscheme.md
    [Authorize]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    public class BaseController : ControllerBase
    {

    }
}