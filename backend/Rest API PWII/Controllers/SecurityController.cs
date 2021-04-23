using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Net;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public SecurityController( UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        ResponseApiError ValidateSignUpModel( SignUpModel signup )
        {
            if (signup.UserName == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User name not valid"
                };

            if (signup.Email == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Email not valid"
                };

            if (signup.Tag == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Tag not valid"
                };

            if (signup.Password == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Password not valid"
                };
            return null;
        }

        ResponseApiError ValidateLoginModel( LoginModel login )
        {
            if (login.UserName == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Username not valid"
                };

            if (login.Password == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Password not valid"
                };

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser( [FromBody] SignUpModel signUpModel )
        {
            try
            {
                var err = ValidateSignUpModel( signUpModel );

                if (err != null)
                    return StatusCode( err.HttpStatusCode, err);

                var result = await _userManager.CreateAsync(new User
                {
                    Email       = signUpModel.Email,
                    UserName    = signUpModel.UserName,
                    Tag         = signUpModel.Tag
                }, signUpModel.Password ) ;

                if ( !result.Succeeded )
                    return StatusCode( 500, "User create failed" );

                return Ok( "User create successful" );
            }
            catch ( Exception ex )
            {
                return StatusCode( 500, ex.Message );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login( [FromBody] LoginModel login )
        {
            try
            {
                var err = ValidateLoginModel( login );

                if (err != null)
                    return StatusCode( err.HttpStatusCode, err );

                bool IsEmail = true;

                var user = await  _userManager.FindByEmailAsync( login.UserName );
                var userUN = await _userManager.FindByNameAsync( login.UserName.ToUpper() );

                if ( user == null && userUN == null )
                    return StatusCode(404);

                if ( user == null )
                    IsEmail = false;
                
                var result = await _signInManager.CheckPasswordSignInAsync(IsEmail ? user : userUN , login.Password, false );

                if (!result.Succeeded)
                    return StatusCode(404);

                var secretKey = _configuration.GetValue<string>("SecretKey");

                var key = Encoding.ASCII.GetBytes( secretKey );

                var claims = new ClaimsIdentity( new[] {
                    new Claim( ClaimTypes.NameIdentifier, IsEmail ? user.Id : userUN.Id),
                    new Claim( ClaimTypes.Name, IsEmail ? user.UserName : userUN.UserName )
                });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials( new SymmetricSecurityKey( key ), SecurityAlgorithms.HmacSha256Signature )
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = tokenHandler.WriteToken(createdToken),
                        Message = "Login Successful"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //TODO terminar modificacion de contrasena
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangePassword( string id, [FromBody] string Password ){
            try
            {
                var err = ValidateLoginModel(login);

                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                var user = await _userManager.FindByEmailAsync(login.UserName);

                if (user == null)
                    return StatusCode(404);

                var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

                if (!result.Succeeded)
                    return StatusCode(404);

                var secretKey = _configuration.GetValue<string>("SecretKey");

                var key = Encoding.ASCII.GetBytes(secretKey);


                var claims = new ClaimsIdentity(new[] {
                    new Claim( ClaimTypes.NameIdentifier, user.Id),
                    new Claim( ClaimTypes.Name, user.UserName )
                });

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(tokenHandler.WriteToken(createdToken));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }*/
    }
}