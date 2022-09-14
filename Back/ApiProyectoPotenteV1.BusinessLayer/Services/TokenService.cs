using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.DataLayer.Models;
using ApiProyectoPotenteV1.DataLayer.Persistence;
using ApiProyectoTienda.DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.BusinessLayer.Services
{
    public class TokenService : ITokenService
    {
        private readonly BDContext _context;
        public IConfiguration _configuration;

        public TokenService(BDContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<Cliente> Login(LoginModelo loginDatos)
        {
            var clientes = _context.Clientes2.Where(cliente => cliente.email == loginDatos.email && cliente.contraseña == loginDatos.contraseña).ToList();

            return clientes;

        }
    }
}
