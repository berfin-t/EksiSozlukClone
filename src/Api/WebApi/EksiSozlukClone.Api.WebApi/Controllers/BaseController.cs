using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EksiSozlukClone.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    public Guid? UserId =>  Guid.NewGuid(); // (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}
