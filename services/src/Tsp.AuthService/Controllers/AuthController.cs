﻿using Auth;
using Auth.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tsp.AuthService.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtAuthorizeHandler _authorizeHandler;

        public AuthController(IJwtAuthorizeHandler authorizeHandler)
        {
            _authorizeHandler = authorizeHandler;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public JsonResult Login([FromBody] JwtUserDto user)
        {
            _authorizeHandler.AuthorizeUser(user, HttpContext);

            return Json(user);
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public JsonResult Login([FromHeader] string authorization)
        {
            var aa = _authorizeHandler.TokenExpired(authorization.Substring(7));
            return Json(new { status = 200 });
        }
    }
}