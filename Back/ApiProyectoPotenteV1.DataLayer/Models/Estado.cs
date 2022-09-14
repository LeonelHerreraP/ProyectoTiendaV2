using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.DataLayer.Models
{
    public class Estado
    {
        public int id { get; set; }
        public string? estado { get; set; }
    }
}
