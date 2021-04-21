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
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;

        public SecurityController( UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration configuration )
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
                    Message = "Nombre de usuario no valido"
                };

            if (signup.Email == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Email no valido"
                };

            if (signup.Tag == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Tag no valido"
                };

            if (signup.Password == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Contraseña no valido"
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
                    Message = "Nombre de usuario no valido"
                };

            if (login.Password == null)
                return new ResponseApiError
                {
                    Code = 400,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Contraseña no valido"
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

                var result = await _userManager.CreateAsync(new Usuario
                {
                    Email       = signUpModel.Email,
                    UserName    = signUpModel.UserName,
                    Tag         = signUpModel.Tag
                }, signUpModel.Password ) ;

                if ( !result.Succeeded )
                    return StatusCode( 500, "Error en la creacion del usuario" );

                return Ok( "Usuario creado exitosamente" );
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

                var user = await  _userManager.FindByEmailAsync( login.UserName );

                if (user == null)
                    return StatusCode(404);

                var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false );

                if (!result.Succeeded)
                    return StatusCode(404);

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

                return Ok(tokenHandler.WriteToken(createdToken));
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