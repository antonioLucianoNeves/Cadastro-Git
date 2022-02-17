using CadastroDeClientes.Data;
using CadastroDeClientes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeClientes.Controllers
{
    public class ClientesController : Controller
    {
        private readonly BancoContext _contexto;

        public ClientesController(BancoContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Clientes.ToListAsync());
        }

        [HttpGet]
        public IActionResult CriarCliente()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CriarCliente(ClientesModel clientes)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(clientes);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else return View(clientes); 
        }
        [HttpGet]
        public IActionResult EditarCliente(int? id)
        {
            if(id != null)
            {
                ClientesModel clientes = _contexto.Clientes.Find(id);
                return View(clientes);
            }
            else return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditarCliente(int id, ClientesModel clientes)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(clientes);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else return View(clientes);
                
            }
            else return NotFound();
        }
        [HttpGet]
        public IActionResult ExcluirCliente(int? id)
        {
            if (id != null)
            {
                ClientesModel clientes = _contexto.Clientes.Find(id);
                return View(clientes);
            }
            else return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ExcluirCliente(int id, ClientesModel clientes)
        {
            if (id != null)
            {
                _contexto.Remove(clientes);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();
        }
    }
}

