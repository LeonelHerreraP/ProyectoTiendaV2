using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.DataLayer.Models
{
    public class Producto
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public int? cantidad { get; set; }
        public int precio { get; set; }
        public string? imagenUrl { get; set; }
    }
}
