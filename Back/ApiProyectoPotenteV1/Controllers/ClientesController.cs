using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiProyectoPotenteV1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        public ClientesController(ILogger<ProductosController> logger)
        {
            _logger = logger;

        }


        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<List<Cliente>>> Listar([FromServices] IClientesService cliente)
        {
            _logger.LogWarning("Clientes/Listar/ -> GET");
            var response = await cliente.Listar();
            return Ok(response);


        }

        [HttpGet]
        [Route("BuscarPorEmail/{email}")]
        public async Task<ActionResult<Cliente>> BuscarPorEmail([FromServices] IClientesService cliente,string email)
        {

            _logger.LogWarning("Clientes/BuscarPorEmail/ -> GET ParametroEmail: " + email);
            var response = await cliente.BuscarPorEmail(email);
            return Ok(response);
        }

        [HttpGet]
        [Route("Login/{email}/{contraseña}")]
        public bool Login([FromServices] IClientesService cliente, string email, string contraseña)
        {
            _logger.LogWarning("Clientes/Login/ -> GET ParametroEmailContra: " + email + " - " + contraseña);
            var response = cliente.Login(email, contraseña);
            return response;
        }

        [HttpPost]
        [Route("AgregarCliente")]
        public async Task<ActionResult> AgregarCliente([Bind("nombre, email, password, direccion")] Cliente cliente, [FromServices] IClientesService clientes)
        {
            _logger.LogWarning("Clientes/AgregarCliente/ -> POST ParametroVM: " + cliente);
            var response = await clientes.AgregarCliente(cliente);
            return Ok(response);
        }

    }
}
