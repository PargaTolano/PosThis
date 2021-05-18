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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

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

        private ResponseApiError ValidateSignUpModel( SignUpModel signup )
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

        private ResponseApiError ValidateLoginModel( LoginModel login )
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

        private async Task<ResponseApiError> ValidateExists( string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Username not valid"
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

                var user   = await _userManager.FindByEmailAsync( login.UserName );
                var userUN = await _userManager.FindByNameAsync ( login.UserName.ToUpper() );

                user = user ?? userUN;

                if ( user == null )
                    return StatusCode(404);

                var result = await _signInManager.CheckPasswordSignInAsync( user, login.Password, false );

                if (!result.Succeeded)
                    return StatusCode( (int)HttpStatusCode.NotFound );

                var secretKey = _configuration.GetValue<string>("SecretKey");

                var key = Encoding.ASCII.GetBytes( secretKey );

                var claims = new ClaimsIdentity( new[] {
                    new Claim( ClaimTypes.NameIdentifier, user.Id),
                    new Claim( ClaimTypes.Name, user.UserName )
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
                        Code = (int)HttpStatusCode.OK,
                        Data = new UserAuthModel { 
                            Id              = user.Id,
                            UserName        = user.UserName,
                            ProfilePicPath  = user.ProfilePic   != null ? $"{Request.Scheme}://{Request.Host}{Request.PathBase}/static/{user.ProfilePic.Name}" : "",
                            Token           = tokenHandler.WriteToken(createdToken)
                        },
                        Message = "Login Successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode( ( int ) HttpStatusCode.InternalServerError, ex.Message );
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordModel model ){
            try
            {
                var err = await ValidateExists(id);

                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                var user = await _userManager.FindByIdAsync(id);

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                    return StatusCode(500);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}