using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.BusinessLayer.ViewModels;
using ApiProyectoPotenteV1.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProyectoPotenteV1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        
        public ProductosController(ILogger<ProductosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<List<Producto>>> Listar([FromServices] IProductosService prod)
        {
            _logger.LogWarning("Productos/Listar/ -> GET");
            var response = await prod.Listar();
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("AgregarProducto")]
        public async Task<ActionResult> AgregarProducto([Bind("nombre, cantidad, precio, imagenUrl")] Producto producto, [FromServices] IProductosService prod)
        {
            _logger.LogWarning("Productos/AgregarProducto/ -> POST ParametroVM: " + producto);
            var response = await prod.AgregarProducto(producto);
            return Ok(response);
        }

        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public async Task<ActionResult<Producto>> BuscarPorId(int id, [FromServices] IProductosService prod)
        {
            _logger.LogWarning("Productos/BuscarPorId/ -> GET ParametroId: " + id);
            var response = await prod.BuscarPorId(id);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("CambioCantidad")]
        public async Task<ActionResult> CambioCantidad(CambiarCantidadVM vm, [FromServices] IProductosService prod)
        {
            _logger.LogWarning("Productos/CambioCantidad/ -> POST ParametroVm: " + vm);
            var response = await prod.CambioCantidad(vm);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("DisminuirUnoCantidad/{id}")]
        public async Task<bool> DisminuirUnoCantidad(int id, [FromServices] IProductosService prod)
        {
            _logger.LogWarning("Productos/CambioCantidad/ -> POST ParametroId: " + id);
            var response = await prod.DisminuirUnoCantidad(id);
            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("ProductosCarrito_Checkout/{idCliente}/{idEstado}")]
        public async Task<ActionResult<List<ProductoYIdSoli>>> ProductosCarrito(int idCliente,int idEstado, [FromServices] IProductosService prod)
        {
            _logger.LogWarning("Productos/ProductosCarrito_Checkout/ -> GET idCliente: " + idCliente+"idEstado: "+idEstado);
            var response = await prod.ProductosCarrito_Checkout(idCliente, idEstado);
            return Ok(response);

        }

        [Authorize]
        [HttpGet]
        [Route("ValorAPagar/{idCliente}")]
        public async Task<int> CantidadPago(int idCliente, [FromServices] IProductosService prod)
        {
            _logger.LogWarning("Productos/ProductosCarrito>/ -> GET idCliente: " + idCliente);
            var response = await prod.CantidadPago(idCliente);
            return response;
        }



    }
}
