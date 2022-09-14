using ApiProyectoPotenteV1.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.BusinessLayer.Interfaces
{
    public interface ISolicitudesService
    {
        Task<List<Solicitud>> Listar();
        Task<bool> EliminarSolicitud(int id);

        Task<bool> AgregarSolicitud([Bind("id_estado, id_cliente, id_producto")] Solicitud solicitud);

        Task<bool> CambioEstado([Bind("id, id_estado")] Solicitud solicitud);

        Task<bool> NextPago(int idCliente);
        Task<bool> PagoProductos(int idCliente);
    }
}
