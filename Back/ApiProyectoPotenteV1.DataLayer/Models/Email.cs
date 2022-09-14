using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.DataLayer.Models
{
    public class Email
    {

        public string? nombre { get; set; }
        public string? email { get; set; }
        public List<ProductoYIdSoli> Productos { get; set; }
    }
}
