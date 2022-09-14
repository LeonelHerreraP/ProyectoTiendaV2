using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.BusinessLayer.ViewModels;
using ApiProyectoPotenteV1.DataLayer.Models;
using ApiProyectoPotenteV1.DataLayer.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProyectoPotenteV1.BusinessLayer.Services
{
    public class ProductosService : IProductosService
    {
        private readonly BDContext _context;

        public ProductosService(BDContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarProducto([Bind(new[] { "nombre, cantidad, precio, imagenUrl" })] Producto producto)
        {
            try
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                _context.Productos2.Add(producto);
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Producto> BuscarPorId(int id)
        {
            var productos = await _context.Productos2.FindAsync(id);
            return productos;
        }

        public async Task<bool> CambioCantidad(CambiarCantidadVM vm)
        {
            var producto = await _context.Productos2.FindAsync(vm.id);
            if (producto != null)
            {
                producto.cantidad = vm.cantidad;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DisminuirUnoCantidad(int id)
        {
            var producto = await _context.Productos2.FindAsync(id);
            if (producto != null)
            {
                producto.cantidad = producto.cantidad - 1;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Producto>> Listar()
        {
            var productos = await _context.Productos2.ToListAsync();
            return productos;
        }

        public async Task<List<ProductoYIdSoli>> ProductosCarrito_Checkout(int idCliente, int idEstado)
        {
            var solicitudes = await _context.Solicitudes2.ToListAsync();
            solicitudes = solicitudes.Where(solicitud => solicitud.id_cliente == idCliente && solicitud.id_estado == idEstado).ToList();

            List<ProductoYIdSoli> Productos = new List<ProductoYIdSoli>();
            foreach (var solicitud in solicitudes)
            {
                var producto = await _context.Productos2.FindAsync(solicitud.id_producto);
                ProductoYIdSoli productoRemix = new ProductoYIdSoli();
                productoRemix.id = producto.id;
                productoRemix.nombre = producto.nombre;
                productoRemix.precio = producto.precio;
                productoRemix.imagenUrl = producto.imagenUrl;
                productoRemix.id_solicitud = solicitud.id;
                Productos.Add(productoRemix);
            }

           

            return Productos;
        }

        public async Task<int> CantidadPago(int idCliente)
        {
            var solicitudes = await _context.Solicitudes2.ToListAsync();
            solicitudes = solicitudes.Where(solicitud => solicitud.id_cliente == idCliente && solicitud.id_estado == 2).ToList();
            var cantidadPago = 0;
            foreach (var solicitud in solicitudes)
            {
                var producto = await _context.Productos2.FindAsync(solicitud.id_producto);
                cantidadPago += producto.precio;
            }

            return cantidadPago;
        }
    }
}
