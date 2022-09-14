using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.DataLayer.Models
{
    public class Solicitud
    {
        public int id { get; set; }
        public int id_estado { get; set; }
        public int id_cliente { get; set; }
        public int id_producto { get; set; }


    }
}
