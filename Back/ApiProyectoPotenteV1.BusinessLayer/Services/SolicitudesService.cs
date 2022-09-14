using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.DataLayer.Models;
using ApiProyectoPotenteV1.DataLayer.Persistence;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Text;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace ApiProyectoPotenteV1.BusinessLayer.Services
{
    public class SolicitudesService : ISolicitudesService
    {
        private readonly BDContext _context;
        public SolicitudesService(BDContext context)
        {
            _context = context;
        }

        public async Task<bool> EliminarSolicitud(int id)
        {
            try
            {
                var soli = await _context.Solicitudes2.SingleOrDefaultAsync(m => m.id == id);

                _context.Solicitudes2.Remove(soli);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> AgregarSolicitud([Bind(new[] { "id_estado, id_cliente, id_producto" })] Solicitud solicitud)
        {
            _context.Solicitudes2.Add(solicitud);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> CambioEstado([Bind(new[] { "id, id_estado" })] Solicitud solicitud)
        {
            var solicitudResultado = await _context.Solicitudes2.FindAsync(solicitud.id);
            if (solicitudResultado != null)
            {
                solicitudResultado.id_estado = solicitud.id_estado;
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Solicitud>> Listar()
        {
            var solicitudes = await _context.Solicitudes2.ToListAsync();
            return solicitudes;
        }

        public async Task<bool> NextPago(int idCliente)
        {
            try
            {
                var solicitudes = await _context.Solicitudes2.ToListAsync();
                solicitudes = solicitudes.Where(solicitud => solicitud.id_cliente == idCliente && solicitud.id_estado == 1).ToList();

                foreach (var solicitud in solicitudes)
                {
                    solicitud.id_estado++;
                }
                await _context.SaveChangesAsync();


                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> PagoProductos(int idCliente)
        {
            try
            {
                var solicitudes = await _context.Solicitudes2.ToListAsync();
                solicitudes = solicitudes.Where(solicitud => solicitud.id_cliente == idCliente && solicitud.id_estado == 2).ToList();

                foreach (var solicitud in solicitudes)
                {
                    solicitud.id_estado++;
                }
                await _context.SaveChangesAsync();

                var cliente = await _context.Clientes2.FindAsync(idCliente);
                var solicitudesPagadas = _context.Solicitudes2.Where(solicitud => solicitud.id_cliente == idCliente && solicitud.id_estado == 3).ToList();
                List<Producto> Productos = new List<Producto>();
                foreach (var solicitud in solicitudesPagadas)
                {
                    var producto = await _context.Productos2.FindAsync(solicitud.id_producto);
                    Productos.Add(producto);
                }

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("eliktienda@hotmail.com"));
                email.To.Add(MailboxAddress.Parse(cliente.email));
                email.Subject = $"Compra en Elik Tienda {cliente.nombre}";

                string dir = Directory.GetCurrentDirectory();
                dir += "\\VistaEmail\\correo.html";
                string template = await File.ReadAllTextAsync(dir);

                var items = "";

                foreach(var producto in Productos)
                {
                    items += $"<tr style=\"border-bottom: 1px solid rgba(0,0,0,.05);\">\r\n                            <td valign=\"middle\" width=\"80%\" style=\"text-align:left; padding: 0 2.5em;\">\r\n                                <div class=\"product-entry\">\r\n                                                                       <div class=\"text\">\r\n                                        <h3>{producto.nombre}</h3>\r\n                                                                                                                  </div>\r\n                                </div>\r\n                            </td>\r\n                            <td valign=\"middle\" width=\"20%\" style=\"text-align:left; padding: 0 2.5em;\">\r\n                                <span class=\"price\" style=\"color: #000; font-size: 20px;\">${producto.precio}</span>\r\n                            </td>\r\n                        </tr>";
                }
                template = template.Replace("{nombre}", cliente.nombre);
                template = template.Replace("{cuerpa}", items);

                email.Body = new TextPart(TextFormat.Html) { Text =  template};
                //async
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("eliktienda@hotmail.com", "velake123@");
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
