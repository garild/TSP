using Auth;
using Auth.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;
using System;
using Elastic.Apm;
using Elastic.Apm.Api;

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
            // TODO Integrate with EF DB to save user
            using var httpClient = new HttpClient { BaseAddress = new Uri("http://o2.pl") };
            httpClient.GetAsync("/");


            using var httpClient_google = new HttpClient { BaseAddress = new Uri("http://google.pl") };
            httpClient_google.GetAsync("/");

            //Method 1

           Agent.Tracer.Capture(()=> _authorizeHandler.AuthorizeUser(user, HttpContext));

            //Method2 
            Agent.Tracer.CurrentTransaction.CaptureSpan("Method  2", "Authhandler 2", () =>
           _authorizeHandler.AuthorizeUser(user, HttpContext));

            return Json(user);
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
}