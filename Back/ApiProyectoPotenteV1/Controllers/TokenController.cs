using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.DataLayer.Models;
using ApiProyectoPotenteV1.DataLayer.Persistence;
using ApiProyectoTienda.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ApiProyectoPotenteV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        public IConfiguration _configuration;
        private readonly BDContext _context;

        public TokenController(IConfiguration configuration, BDContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([Bind("email, contraseña")] LoginModelo loginDatos, [FromServices] ITokenService tokenS)
        {
            //var resultado = await _context.Clientes2.Where(cliente => cliente.email == email && cliente.contraseña == contraseña).ToListAsync();
            var resultado = tokenS.Login(loginDatos);
            if (resultado.Count == 0)
            {
                return Ok(false);
            }
            else
            {

                var claims = new[] {
                        //new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("id_cliente", resultado.FirstOrDefault().id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:ValidIssuer"],
                    audience: _configuration["Jwt:ValidAudience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn
                    );

                var tokenReal = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenReal.ToList());
            }
        }
    }
}
