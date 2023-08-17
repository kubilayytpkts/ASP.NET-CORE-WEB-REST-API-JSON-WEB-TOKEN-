using IndataProje.Models;
using IndataProje.NewFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace IndataProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserSupervisionController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        public UserSupervisionController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;  
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody]User userInformation)
        {
            var user = IdentityTest(userInformation);
            if (user == null)
                return NotFound("User not found");

            var token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            if (_jwtSettings.Key == null)
                throw new Exception("null cannot be a value");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimArray = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName!),
                new Claim(ClaimTypes.Role,user.Role!)
            };

            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimArray,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User? IdentityTest(User userInformation)
        {
            return UserList.Users.FirstOrDefault
                (m => m.UserName?.ToLower() == userInformation.UserName.ToLower()
                && m.password == userInformation.password);
        }
    }


}
