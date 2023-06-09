using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;
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
                EmailConfirmed = false
            };

            var isCreated = await _userManager.CreateAsync(newUser, registrationRequest.Password);

            if (isCreated.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                
                var emailBody = "Please Confirm your Email address! <a href=\"#URL#\"> Click Here </a>";
                
                // https://localhost:8080/authentication/verifyemail/userid=asdad&code=asdadasd
                var callbackUrl = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Authentication",
                    new {userId = newUser.Id, code = code});

                var body = emailBody.Replace("#URL#",callbackUrl);
                
                // send email
                var result = SendEmail(body, newUser.Email);
                
                if(result)
                    return Ok("Thank you for choosing TrustGuard! Please confirm your email address by clicking the link below.");
                
                return Ok("An issue occurred! Please request a new email confirmation link!");

                //Generate the token
                // var token = GenerateJwtToken(newUser);
                // return Ok(new AuthResult()
                // {
                //     Result = true,
                //     Token = token
                // });
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

    [Route("ConfirmEmail")]
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null && code == null)
        {
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Invalid Email Confirmation URL"               
                }
            });
        }

        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
        {
            return BadRequest(new AuthResult()
            {
                Errors = new List<string>()
                {
                    "Invalid Email Parameters"               
                }
            });
        }

        code = Encoding.UTF8.GetString(Convert.FromBase64String(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        var status = result.Succeeded
            ? "Thank you for confirming your email"
            : "Your email is not confirmed, please try again later";
        
        return Ok(status);
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

            if (!existingUser.EmailConfirmed)
            {
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Email needs to be confirmed!"
                    },
                    Result = false
                });
            }

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

    private bool SendEmail(string body, string email)
    {
        // create client
        var client = new RestClient("https://api.mailgun.net/v3");
        
        var request = new RestRequest("", Method.Post);
        
        client.Authenticator =
            new HttpBasicAuthenticator("api", _configuration.GetSection("EmailConfig:API_KEY").Value);
        

        request.AddParameter("domain", "sandboxd03b10b0dfa2499db9fc4378f3a903fb.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter("from",
            "TrustGuard Service <postmaster@sandboxd03b10b0dfa2499db9fc4378f3a903fb.mailgun.org>");
        request.AddParameter("to", "ermalkk14@gmail.com");
        request.AddParameter("subject", "Email Verification");
        request.AddParameter("text", body);
        request.Method = Method.Post;

        var response = client.Execute(request);
        return response.IsSuccessful;

    }

}