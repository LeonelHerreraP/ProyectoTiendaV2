using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.DataLayer.Models
{
    public class ProductoYIdSoli
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public int precio { get; set; }
        public string? imagenUrl { get; set; }
        public int id_solicitud { get; set; }

    }
}
