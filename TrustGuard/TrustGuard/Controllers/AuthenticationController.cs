using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrustGuard.Domain.Configurations;
using TrustGuard.Domain.DTOs;
using TrustGuard.Domain.Models;

namespace TrustGuard.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    //private readonly JwtConfig _jwtConfig;
    private readonly IConfiguration _configuration;

    public AuthenticationController(UserManager<IdentityUser> userManager, 
                                    IConfiguration configuration
                                    //JwtConfig jwtConfig
                                    )
    {
        _userManager = userManager;
        _configuration = configuration;
        //_jwtConfig = jwtConfig;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDTO registrationRequest)
    {
        //validate the incoming request
        if (ModelState.IsValid)
        {
            // validate the incoming request
            var userExist = await _userManager.FindByEmailAsync(registrationRequest.Email);

            if (userExist != null)
            {
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Email already exist"
                    }
                });
            }
            
            // create a user
            var newUser = new IdentityUser()
            {
                Email = registrationRequest.Email,
                UserName = registrationRequest.Email,

            };

            var isCreated = await _userManager.CreateAsync(newUser, registrationRequest.Password);

            if (isCreated.Succeeded)
            {
                //Generate the token
                var token = GenerateJwtToken(newUser);
                return Ok(new AuthResult()
                {
                    Result = true,
                    Token = token
                });
            }
            
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Server error"               
                }
            });
        }

        return BadRequest();

    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO loginRequest)
    {
        if (ModelState.IsValid)
        {
            // Check if the user exist
            var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (existingUser == null)
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid Payload"
                    },
                    Result = false
                });

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);

                if (!isCorrect)
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid Credentials"
                        },
                        Result = false
                    });

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new AuthResult()
                {
                    Token = jwtToken,
                    Result = true
                });
        }

        return BadRequest(new AuthResult()
        {
            Errors = new List<string>()
            {
                "Invalid Payload"
            },
            Result = false
        });
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var jwtTokenHandeler = new JwtSecurityTokenHandler();

        // storing the secret key 
        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
        
        // token Descriptor
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            }),

            // adding token expiry time
            Expires = DateTime.Now.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandeler.CreateToken(tokenDescriptor);
        return jwtTokenHandeler.WriteToken(token);
    }

}