using ApiProyectoPotenteV1.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProyectoPotenteV1.BusinessLayer.Interfaces
{
    public interface IClientesService
    {

        Task<List<Cliente>> Listar();

        Task<List<Cliente>> BuscarPorEmail(string email);

        bool Login(string email, string contraseña);


        Task<bool> AgregarCliente([Microsoft.AspNetCore.Mvc.Bind("nombre, email, password, direccion")] Cliente cliente);

    }
}
