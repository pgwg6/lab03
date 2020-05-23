using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab03.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [Route("[action]")]
        public IActionResult SignInGoogle()
        {
            return new ChallengeResult(
                GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(SignInCallback)/*, new { va = "asdf" }*/)
                });
        }

        [Route("[action]")]
        public IActionResult SignInCallback()
        {
            return Ok("Signed in");
        }
    }
}