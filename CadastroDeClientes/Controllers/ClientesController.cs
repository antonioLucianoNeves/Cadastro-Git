using CadastroDeClientes.Data;
using CadastroDeClientes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.WebPages.Html;

namespace CadastroDeClientes.Controllers
{
    public class ClientesController : Controller
    {
        private readonly BancoContext _contexto;

        public ClientesController(BancoContext contexto)
        {
            _contexto = contexto;
        }
        public ActionResult Index(string Pesquisar = "")
        {

            var q = _contexto.Clientes.AsQueryable();
            if (!string.IsNullOrEmpty(Pesquisar))
                q = q.Where(c => c.Nome.Contains(Pesquisar));
            q = q.OrderBy(c => c.Nome);

            return View(q.ToList());
        }
        public ActionResult SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = _contexto.Clientes.FindAsync(minDate, maxDate);
            return View(result);
        }
        [HttpPost]
        public ActionResult Index(string recebeNome, int recebeOpcao)
        {
            try
            {
                ClientesModel dao = new ClientesModel();
                IQueryable<BancoContext> sql;
                sql = null;

                if (recebeOpcao == 1)
                {
                    var q = _contexto.Clientes.AsQueryable();
                    if (!string.IsNullOrEmpty(recebeNome))
                        q = q.Where(c => c.Tipo.ToString().Contains(recebeNome));
                    q = q.OrderBy(c => c.Tipo);
                  
                    TempData["opcao1"] = "cancelado";
                }
                return View(sql.ToList());
                if (recebeOpcao == 2)
                {
                    var q = _contexto.Clientes.AsQueryable();
                    if (!string.IsNullOrEmpty(recebeNome))
                        q = q.Where(c => c.Tipo.ToString().Contains(recebeNome));
                    q = q.OrderBy(c => c.Tipo);

                    TempData["opcao2"] = "bloqueado";
                }
                return View(sql.ToList());

                if (recebeOpcao == 3)
                {
                    var q = _contexto.Clientes.AsQueryable();
                    if (!string.IsNullOrEmpty(recebeNome))
                        q = q.Where(c => c.Tipo.ToString().Contains(recebeNome));
                    q = q.OrderBy(c => c.Tipo);

                    TempData["opcao3"] = "padrao";
                }

                return View(sql.ToList());

            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Erro na gravação dos dados " + ex.Message;
            }
            return View();

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

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Padrao", Value = "0" });

            items.Add(new SelectListItem { Text = "Bloqueado", Value = "1" });

            items.Add(new SelectListItem { Text = "Cancelado", Value = "2", Selected = true });

            ViewBag.MovieType = items;

            return View(clientes.Tipo);
        }
        [HttpGet]
        public IActionResult EditarCliente(int? id)
        {
            if (id != null)
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
        [HttpGet]
        public async Task<int> Get(int cont)
        {
            var contador = _contexto.Update(cont);

            return cont;
        }


        [HttpPost]
        public async Task Post(int cont)
        {
            var contador = _contexto.Update(cont);
            await contador.GetDatabaseValuesAsync();
        }
        
    }
}

