using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Practice_13.Business.Services.Interfaces;
using Practice_13.Models.Response;
using Practice_13.Models.ViewModels;
using Practice_13.Utils;

namespace Practice_13.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        IAccountsService accountsService;
        ITokenService tokenService;
        public AuthController(IAccountsService accountsService, ITokenService tokenService)
        {
            this.accountsService = accountsService;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var srcAcc = await accountsService.GetAccount(model.Username);

            if (srcAcc is null || !srcAcc.Password.Equals(model.Password))
            {
                return Unauthorized();
            }

            var response = new JwtResponseModel
            {
                AccessToken = tokenService.CreateToken(srcAcc),
                Username = srcAcc.Username
            };
            return new JsonResult(response);
        }
    }
}
