using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiProyectoPotenteV1.DataLayer.Models;
using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ApiProyectoPotenteV1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly ILogger<SolicitudesController> _logger;

        public SolicitudesController(ILogger<SolicitudesController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<List<Solicitud>>> Listar([FromServices] ISolicitudesService solic)
        {
            _logger.LogWarning("Solicitudes/Listar/ -> GET");
            return Ok(await solic.Listar());

        }

        [HttpGet]
        [Route("EliminarSolicitud/{id}")]
        public async Task<ActionResult> EliminarSolicitud(int id, [FromServices] ISolicitudesService solic)
        {

            _logger.LogWarning("Solicitudes/EliminarSolicitud/ -> GET Parametro-Id: " + id);
            await solic.EliminarSolicitud(id);
            return Ok();


        }

        [HttpGet]
        [Route("NextPago/{idCliente}")]
        public async Task<ActionResult> NextPago(int idCliente, [FromServices] ISolicitudesService solic)
        {

            _logger.LogWarning("Solicitudes/NextPago/ -> GET Parametro-IdCliente: " + idCliente);
            await solic.NextPago(idCliente);
            return Ok();


        }

        [HttpGet]
        [Route("PagoProductos/{idCliente}")]
        public async Task<ActionResult> PagoProductos(int idCliente, [FromServices] ISolicitudesService solic)
        {

            _logger.LogWarning("Solicitudes/PagoProductos/ -> GET Parametro-IdCliente: " + idCliente);
            await solic.PagoProductos(idCliente);
            return Ok();


        }

        [HttpPost]
        [Route("AgregarSolicitud")]
        public async Task<ActionResult> AgregarSolicitud([Bind("id_estado, id_cliente, id_producto")] Solicitud solicitud, [FromServices] ISolicitudesService solic)
        {

            _logger.LogWarning("Solicitudes/AgregarSolicitud/ -> POST ParametroVM: " + solicitud);
            await solic.AgregarSolicitud(solicitud);
            return Ok();


        }

        [HttpPost]
        [Route("CambioEstado")]
        public async Task<ActionResult> CambioEstado([Bind("id, id_estado")] Solicitud solicitud, [FromServices] ISolicitudesService solic)
        {
            _logger.LogWarning("Solicitudes/CambioEstado/ -> POST ParametroVm: " + solicitud);
            if (await solic.CambioEstado(solicitud))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        
    }
}
