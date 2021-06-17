using Interno.Interfaces;
using Interno.Models;
using Interno.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interno.Controllers
{
    public class Clientes_cController : Controller
    {
        private ICliente_cService<Cliente_c> cliente_cService;
        private IClienteService<Cliente> clienteService;

        public Clientes_cController()
        {
            this.cliente_cService = new Cliente_cService();
            this.clienteService = new ClienteService();
        }
        // GET: Clientes_c
        public ActionResult Index(int cliente_id = 0, string searchKey = null)
        {
            Cliente cliente;
            Cliente_c cliente_c;
            IEnumerable<Cliente_c> clientes;

            try
            {
                cliente = clienteService.Find(cliente_id);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            if (cliente == null)
            {
                return HttpNotFound();
            }

            ViewBag.Cliente_nome = cliente.CLIENTE_NOME;
            ViewBag.Cliente_cnpj = cliente.CLIENTE_CNPJ;
            ViewBag.cliente_id = cliente.CLIENTE_ID;

            if (cliente_id != 0)
            {
                try
                {
                    cliente_c = cliente_cService.FindByRazao(cliente.CLIENTE_NOME);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                if (cliente_c == null)
                {
                    cliente_c = new Cliente_c(cliente_id, cliente.CLIENTE_NOME);

                    try
                    {
                        cliente_cService.Create(cliente_c);
                    }
                    catch (Exception ex)
                    {
                        TempData["Message"] = ex.Message;
                        return RedirectToAction("Error");
                    }
                }

                try
                {
                    clientes = cliente_cService.IndexByClienteId(cliente_id);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                return View(clientes);
            }

            if (searchKey != null)
            {
                try
                {
                    clientes = cliente_cService.IndexByRazao(searchKey);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                if (clientes.Count() > 0)
                {
                    return View(clientes);
                }
            }

            try
            {
                return View(cliente_cService.Index());
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Create(int cliente_id = 0)
        {
            if (cliente_id == 0)
            {
                return HttpNotFound();
            }

            ViewBag.cliente_id = cliente_id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cliente_c cliente, int cliente_id)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            cliente.CLIENTE_ID = cliente_id;
            cliente.CLIENTE_NOME = cliente.CLIENTE_NOME.ToUpper();

            try
            {
                cliente_cService.Create(cliente);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index", "Clientes_c", new { cliente_id });
        }

        //[HttpDelete]
        [HttpPost]
        [Route("Clientes_c/Delete/{id}")]
        public ActionResult Delete(int id, int? cliente_id)
        {
            Cliente_c cliente_c;

            cliente_c = cliente_cService.Find(id);


            if (cliente_c == null)
            {
                return HttpNotFound();
            }

            try
            {
                cliente_cService.Delete(cliente_c);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index", new { cliente_id = cliente_id });
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}