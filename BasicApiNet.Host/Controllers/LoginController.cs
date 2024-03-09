using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BasicApiNet.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BasicApiNet.Host.Controllers;

/// <summary>
/// Login controller to generate a token.
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    
    /// <summary>
    /// Login method to generate a token.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Login(UserLogin login)
    {
        try
        {
            if (string.IsNullOrEmpty(login.UserName) ||
                string.IsNullOrEmpty(login.Password))
                return BadRequest("Username and/or Password is not correct.");
            if (login.UserName.Equals("admin") &&
                login.Password.Equals("admin123"))
            {
                var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("BasicApicNetSecretKey@UMBRELLA12345"));
                var signinCredentials = new SigningCredentials
                    (secretKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: "UMBRELLA",
                    audience: "http://localhost:5228",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );
                return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest
                ("An error occurred in generating the token");
        }
        return Unauthorized();
    }
}