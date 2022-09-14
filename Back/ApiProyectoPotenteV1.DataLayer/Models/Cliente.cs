using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.DataLayer.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? email { get; set; }
        public string? contraseña { get; set; }
        public string? direccion { get; set; }
    }
}
