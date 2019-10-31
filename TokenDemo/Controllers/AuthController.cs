using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TokenDemo.Data;

namespace TokenDemo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        //we need to inject it 
        private UserManager<ApplicationUser> userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]TokenDemo.Model.LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new[] 
                {
                    new Claim(JwtRegisteredClaimNames.Sub,model.Username),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp,DateTime.Now.AddMinutes(15).ToString()),
                    new Claim("any thing","AbdelSattar Testing")
                };

                var siginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

                //we need to generate a token here 
                //JWT Security token 
                var token = new JwtSecurityToken(//add proberty
                    issuer: "http://abdo.com",
                    audience: "http://abdo.com",
                    expires: DateTime.Now.AddHours(15),
                    claims: claims,
signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(siginKey, SecurityAlgorithms.HmacSha256)

                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}