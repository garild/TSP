using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tsp.Authorization;
using Tsp.Authorization.JWT;

namespace Tsp.AuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IJwtAuthorizeHandler _authorizeHandler;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IJwtAuthorizeHandler authorizeHandler, ILogger<AuthController> logger)
        {
            _authorizeHandler = authorizeHandler;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login([FromBody] JwtUserDto user)
        {
           
           _authorizeHandler.AuthorizeUser(user, HttpContext);

            return Json(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public string Index()
        {
            return "Ocelot work";
        }


        [HttpPost]
        [AllowAnonymous]
        public string Login2([FromBody] User user)
        {
            _logger.LogInformation("@{User}", user);
            return user.Name;
        }

        //[HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Route("[action]")]
        //public JsonResult Login([FromBody] JwtUserDto user)
        //{
        //    //TODO EF inMemory User validate etc

        //    return Json(user);
        //}
    }

    public class User
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}