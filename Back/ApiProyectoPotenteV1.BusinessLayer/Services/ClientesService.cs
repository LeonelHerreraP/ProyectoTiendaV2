using ApiProyectoPotenteV1.BusinessLayer.Interfaces;
using ApiProyectoPotenteV1.DataLayer.Models;
using ApiProyectoPotenteV1.DataLayer.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiProyectoPotenteV1.BusinessLayer.Services
{
    public class ClientesService : IClientesService
    {
        private readonly BDContext _context;

        public ClientesService(BDContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarCliente([Bind(new[] { "nombre, email, password, direccion" })] Cliente cliente)
        {
            try
            {
                _context.Clientes2.Add(cliente);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<List<Cliente>> BuscarPorEmail(string email)
        {
            var clientes = await _context.Clientes2.ToListAsync();
            clientes = clientes.Where(x => x.email == email).ToList();
            return clientes;

        }

        public async Task<List<Cliente>> Listar()
        {
            var clientes = await _context.Clientes2.ToListAsync();
            return clientes;
        }

        public bool Login(string email, string contraseña)
        {
            var clientes = _context.Clientes2.Where(cliente => cliente.email == email && cliente.contraseña == contraseña).ToList();
            if (clientes.Count == 0)
            {

                return false;
            }
            else
            {
                
                return true;
            }
        }
    }
}
