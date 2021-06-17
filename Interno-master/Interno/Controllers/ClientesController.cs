using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interno.Services;
using Interno.Models;
using Interno.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using PagedList;

namespace Interno.Controllers
{
    public class ClientesController : Controller
    {
        private IClienteService<Cliente> clienteService;
        private ICliente_cService<Cliente_c> cliente_cService;

        public ClientesController()
        {
            this.clienteService = new ClienteService();
            this.cliente_cService = new Cliente_cService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Clientes
        [HttpPost]
        public object Index(int? page = 1, string searchKey = null, int pageSize = 10)
        {
            IEnumerable<Cliente> clientes;
            int pageNumber;

            pageNumber = (page ?? 1);

            if (searchKey != "" && searchKey != null)
            {
                clientes = clienteService.IndexByRazao(searchKey);

                if (clientes.Count() > 0)
                {
                    return JsonConvert.SerializeObject(clientes.ToPagedList(pageNumber, pageSize));
                }

                clientes = clienteService.FindByCnpj(searchKey);

                if (clientes.Count() > 0)
                {
                    return JsonConvert.SerializeObject(clientes.ToPagedList(pageNumber, pageSize));
                }
            }
            else
            {
                clientes = clienteService.Index();
            }




            return JsonConvert.SerializeObject(clientes.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [Route("Clientes/getInfoPagination")]
        public string getInfoPagination(int? page, string searchKey = null, int pageSize = 10)
        {
            IEnumerable<Cliente> clientes;
            int pageNumber;
            object response;

            pageNumber = (page ?? 1);

            if (searchKey != null && searchKey != "")
            {
                clientes = clienteService.IndexByRazao(searchKey);

                if (clientes.Count() > 0)
                {
                    response = new { currentPage = pageNumber, pageSize = pageSize, itensCount = clientes.Count(), searchKey = searchKey };

                    return JsonConvert.SerializeObject(response);
                }

                clientes = clienteService.FindByCnpj(searchKey);

                if (clientes.Count() > 0)
                {
                    response = new { currentPage = pageNumber, pageSize = pageSize, itensCount = clientes.Count(), searchKey = searchKey };

                    return JsonConvert.SerializeObject(response);
                }
            }
            else
            {
                clientes = clienteService.Index();
            }


            response = new { currentPage = pageNumber, pageSize, itensCount = clientes.Count(), searchKey };

            return JsonConvert.SerializeObject(response);
        }

        public ActionResult Details(int id)
        {
            try
            {
                return View(clienteService.Find(id));
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            Cliente_c cliente_c;
            Cliente new_cliente;

            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            cliente.CLIENTE_NOME = cliente.CLIENTE_NOME.ToUpper();
            cliente.CLIENTE_CNPJ = cliente.CLIENTE_CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");

            try
            {
                new_cliente = clienteService.Create(cliente);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            cliente_c = new Cliente_c(new_cliente.CLIENTE_ID, new_cliente.CLIENTE_NOME);

            try
            {
                cliente_cService.Create(cliente_c);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Cliente cliente;

            try
            {
                cliente = clienteService.Find(id);
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

            TempData["old_name"] = cliente.CLIENTE_NOME;

            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            Cliente_c cliente_c;
            Cliente new_cliente;

            if (ModelState.IsValid)
            {
                try
                {
                    cliente_c = cliente_cService.FindByRazao(TempData["old_name"].ToString());
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                if (cliente_c != null)
                {
                    try
                    {
                        cliente_cService.Delete(cliente_c);
                    }
                    catch (Exception ex)
                    {
                        TempData["Message"] = ex.Message;
                        return RedirectToAction("Error");
                    }
                }

                try
                {
                    cliente.CLIENTE_NOME = cliente.CLIENTE_NOME.ToUpper();
                    cliente.CLIENTE_CNPJ = cliente.CLIENTE_CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");

                    new_cliente = clienteService.Update(cliente);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                try
                {
                    cliente_c = new Cliente_c(new_cliente.CLIENTE_ID, new_cliente.CLIENTE_NOME);
                    cliente_cService.Create(cliente_c);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(cliente);
            }
        }

        //[HttpDelete]
        [HttpPost]
        [Route("Clientes/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            Cliente cliente;

            try
            {
                cliente = clienteService.Find(id);
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

            try
            {
                cliente_cService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            try
            {
                clienteService.Delete(cliente);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            

            return RedirectToAction("Index");
            // return Index();
        }

        //[HttpPost]
        [Route("Clientes/Find/Cnpj/{cnpj}")]
        public object Find(string cnpj)
        {
            IEnumerable<Cliente> cliente;

            try
            {
                cliente = clienteService.FindByCnpj(cnpj);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            if (cliente.Count() > 0)
            {
                return JsonConvert.SerializeObject(new { Existe = true });
            }

            return JsonConvert.SerializeObject(new { Existe = false });

        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
