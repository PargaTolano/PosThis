using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/]action]")]
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            try
            {
                var result = await _userManager.CreateAsync(new Usuario
                {
                    Email = createUserRequest.Email,
                    UserName = createUserRequest.UserName,
                    Tag = createUserRequest.Tag
                }, createUserRequest.Password) ;

                if ( !result.Succeeded )
                    return StatusCode( 500, "Error en la creacion del usuario" );

                return Ok( "Usuario creado exitosamente" );
            }
            catch ( Exception )
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var user = await (login.isEmail ? _userManager.FindByEmailAsync(login.UserName) : _userManager.FindByNameAsync(login.UserName));

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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}