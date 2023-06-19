using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;
using TrustGuard.Domain;
using TrustGuard.Domain.DTOs;
using TrustGuard.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace TrustGuard.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly UserManager<User> _userManager;
    //private readonly JwtConfig _jwtConfig;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public AuthenticationController(UserManager<User> userManager, 
                                    IConfiguration configuration,
                                    AppDbContext context,
                                    TokenValidationParameters tokenValidationParameters
                                    //JwtConfig jwtConfig
                                    )
    {
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
        _tokenValidationParameters = tokenValidationParameters;
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
            var newUser = new User()
            {
                PersonalId = registrationRequest.PersonalId,
                UserName = registrationRequest.FullName,
                BirthDate = registrationRequest.BirthDate,
                Address = registrationRequest.Address,
                PhoneNumber = registrationRequest.PhoneNumber,
                Email = registrationRequest.Email,
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

        //code = Encoding.UTF8.GetString(Convert.FromBase64String(code));
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

                var jwtToken = await GenerateJwtToken(existingUser);

                return Ok(jwtToken);
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

    private async Task<AuthResult> GenerateJwtToken(IdentityUser user)
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
            Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandeler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandeler.WriteToken(token);
        
        var refreshToken = new RefreshToken()
        {
            JwtId = token.Id,
            Token = GenerateRandomString(23),
            AddedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(6),
            IsRevoked = false,
            IsUsed = false,
            UserId = user.Id
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
        
        return new AuthResult()
        {
            Token = jwtToken,
            RefreshToken = refreshToken.Token,
            Result = true
        };
        
    }
    
    [HttpPost]
    [Route("RefreshToken")] 
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
    {
        if (ModelState.IsValid)
        {
            var result = await VerifyAndGenerateToken(tokenRequest);
            if (result == null)
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid Parameters"
                    },
                    Result = false
                });
            return Ok(result);
            
        }
        return BadRequest(new AuthResult()
        {
            Errors = new List<string>()
            {
                "Invalid Parameters"
            },
            Result = false
        });
        
    }

    private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        try
        {
            _tokenValidationParameters.ValidateLifetime = false; // for testing 

            var tokenInVerfication =
                jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);

                if (result == false)
                {
                    return null;
                }
            }

            var utcExpiryDate = long.Parse(tokenInVerfication.Claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
            if (expiryDate > DateTime.Now)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Expired Token"
                    }
                };
            }

            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);
            
            if (storedToken == null)
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Expired Token"
                    }
                };

            if (storedToken.IsUsed)
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Token is used"
                    }
                };

            if (storedToken.IsRevoked)
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Token is revoked"
                    }
                };


            var jti = tokenInVerfication.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if (storedToken.JwtId != jti)
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Expired Token"
                    }
                };


            if (storedToken.ExpiryDate < DateTime.UtcNow)
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Invalid Token"
                    }
                };

            storedToken.IsUsed = true;
            _context.RefreshTokens.Update(storedToken);
            await _context.SaveChangesAsync();

            var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
            return await GenerateJwtToken(dbUser);

        }
        catch (Exception e)
        {
            return new AuthResult()
            {
                Result = false,
                Errors = new List<string>()
                {
                    "Server Error"
                }
            };
        }

    }
    private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTimeVal;
    }

    private bool SendEmail(string body, string email)
    {
        // create client
        var client = new RestClient("https://api.mailgun.net/v3");
        
        var request = new RestRequest("", Method.Post);
        
        client.Authenticator =
            new HttpBasicAuthenticator("api", _configuration.GetSection("EmailConfig:API_KEY").Value);
        

        request.AddParameter("domain", "sandboxc19fe585f39d43e39f72f6b102f28226.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter("from",
            "TrustGuard Service <postmaster@sandboxc19fe585f39d43e39f72f6b102f28226.mailgun.org>");
        request.AddParameter("to", "Trust Guard <trustguard.ks@gmail.com>");
        request.AddParameter("subject", "Email Verification");
        request.AddParameter("text", body);
        request.Method = Method.Post;

        var response = client.Execute(request);
        return response.IsSuccessful;

    }

    private string GenerateRandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_";

        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

}