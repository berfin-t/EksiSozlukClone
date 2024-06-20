using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EksiSozlukClone.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    //public Guid? UserId =>  new(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    public Guid? UserId
    {
        get
        {
            var val = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return val is null ? null : new Guid(val);
        }
    }
}
