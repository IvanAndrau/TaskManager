using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Text;
using TaskManagerAPI.ViewModel;

namespace TaskManagerAPI.Controllers;

public class LoginViewModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class UserController(SignInManager<IdentityUser> signInManager) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel login)
    {
  /*      var user = await _userService.SignInAsync(login.Email, login.Password);
        if (user != null)
        {
            var role = await _userService.GetUserRole(login.Email);
            var model = new UserMainInfoViewModel()
            {
                Id = new Guid(user.Id),
                Email = login.Email,
                RoleId = role
            };

            var encodedJwt = GetJwtToken(model);
            var response = new { token = encodedJwt };

            return Ok(response);
        }*/
        return Unauthorized();
    }
    
 /* private static string GetJwtToken(UserMainInfoViewModel userMainInfo) //token generation
    {
        var now = DateTime.UtcNow;
        var lifetime = TimeSpan.FromMinutes(45);

        var claims = new Collection<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userMainInfo.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, userMainInfo.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Exp,
                new DateTimeOffset(now.Add(lifetime)).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new(ClaimTypes.Role, userMainInfo.RoleId.ToString())
        };

        //TODO update secret key
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("a6e0cbea095e2e672c8bbdb266d891c4958237f2fdd3586f6ddd557ac9d45db2"));
        var jwt = new JwtSecurityToken("FlexBenefitsIssuer", "FlexBenefitsIssuer", claims,
            now, now.Add(lifetime), new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
 */
}