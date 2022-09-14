using ApiProyectoPotenteV1.BusinessLayer.ViewModels;
using ApiProyectoPotenteV1.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiProyectoPotenteV1.BusinessLayer.Interfaces
{
    public interface IProductosService
    {
        Task<List<Producto>> Listar();

        Task<bool> AgregarProducto([Bind("nombre, cantidad, precio, imagenUrl")] Producto producto);

        Task<Producto> BuscarPorId(int id);

        Task<bool> CambioCantidad(CambiarCantidadVM vm);

        Task<bool> DisminuirUnoCantidad(int id);
        Task<List<ProductoYIdSoli>> ProductosCarrito_Checkout(int idCliente, int idEstado);
        Task<int> CantidadPago(int idCliente);

    }
}
