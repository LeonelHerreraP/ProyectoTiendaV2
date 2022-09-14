using ApiProyectoPotenteV1.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.BusinessLayer.Interfaces
{
    public interface IEmailService
    {

        void Enviar(Email cuerpa);
    }
}
