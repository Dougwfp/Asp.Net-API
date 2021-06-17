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
    public class TransportsController : Controller
    {
        private ITransportService<Transport> transportService;
        private ITransport_cService<Transport_c> transport_cService;

        public TransportsController()
        {
            this.transportService = new TransportService();
            this.transport_cService = new Transport_cService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Transports
        [HttpPost]
        public object Index(int? page = 1, string searchKey = null, int pageSize = 10)
        {
            IEnumerable<Transport> transports;
            int pageNumber;

            pageNumber = (page ?? 1);

            if (searchKey != "" && searchKey != null)
            {
                transports = transportService.IndexByRazao(searchKey);

                if (transports.Count() > 0)
                {
                    return JsonConvert.SerializeObject(transports.ToPagedList(pageNumber, pageSize));
                }

                transports = transportService.FindByCnpj(searchKey);

                if (transports.Count() > 0)
                {
                    return JsonConvert.SerializeObject(transports.ToPagedList(pageNumber, pageSize));
                }
            } else
            {
                transports = transportService.Index();
            }


            

            return JsonConvert.SerializeObject(transports.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [Route("Transports/getInfoPagination")]
        public string getInfoPagination(int? page, string searchKey = null, int pageSize = 10)
        {
            IEnumerable<Transport> transports;
            int pageNumber;
            object response;

            pageNumber = (page ?? 1);

            if (searchKey != null && searchKey != "")
            {
                transports = transportService.IndexByRazao(searchKey);

                if (transports.Count() > 0)
                {
                    response = new { currentPage = pageNumber, pageSize = pageSize, itensCount = transports.Count(), searchKey = searchKey };

                    return JsonConvert.SerializeObject(response);
                }

                transports = transportService.FindByCnpj(searchKey);

                if (transports.Count() > 0)
                {
                    response = new { currentPage = pageNumber, pageSize = pageSize, itensCount = transports.Count(), searchKey = searchKey };

                    return JsonConvert.SerializeObject(response);
                }
            }
            else
            {
                transports = transportService.Index();
            }
            
     
            response = new { currentPage = pageNumber, pageSize, itensCount = transports.Count(), searchKey };

            return JsonConvert.SerializeObject(response);
        }

        public ActionResult Details(int id)
        {
            try
            {
                return View(transportService.Find(id));
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
        public ActionResult Create(Transport transport)
        {
            Transport_c transport_c;
            Transport new_transport;

            if (!ModelState.IsValid)
            {
                return View(transport);
            }

            transport.TRANSP_NOME = transport.TRANSP_NOME.ToUpper();
            transport.TRANSP_CNPJ = transport.TRANSP_CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");

            try
            {
                new_transport = transportService.Create(transport);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            transport_c = new Transport_c(new_transport.TRANSP_ID, new_transport.TRANSP_NOME);

            try
            {
                transport_cService.Create(transport_c);
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
            Transport transport;

            try
            {
                transport = transportService.Find(id);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            if (transport == null)
            {
                return HttpNotFound();
            }

            TempData["old_name"] = transport.TRANSP_NOME;

            return View(transport);
        }

        [HttpPost]
        public ActionResult Edit(Transport transport)
        {
            Transport_c transport_c;
            Transport new_transport;

            if (ModelState.IsValid)
            {
                try
                {
                    transport_c = transport_cService.FindByRazao(TempData["old_name"].ToString());
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                if(transport_c != null)
                {
                    try
                    {
                        transport_cService.Delete(transport_c);
                    }
                    catch (Exception ex)
                    {
                        TempData["Message"] = ex.Message;
                        return RedirectToAction("Error");
                    }
                }

                try
                {
                    transport.TRANSP_NOME = transport.TRANSP_NOME.ToUpper();
                    transport.TRANSP_CNPJ = transport.TRANSP_CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");

                    new_transport = transportService.Update(transport);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                try
                {
                    transport_c = new Transport_c(new_transport.TRANSP_ID, new_transport.TRANSP_NOME);
                    transport_cService.Create(transport_c);
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
                return View(transport);
            }
        }

        //[HttpDelete]
        [HttpPost]
        [Route("Transports/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            Transport transport;

            try
            {
                transport = transportService.Find(id);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            if (transport == null)
            {
                return HttpNotFound();
            }

            try
            {
                transportService.Delete(transport);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            try
            {
                transport_cService.Delete(id);
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
        [Route("Transports/Find/Cnpj/{cnpj}")]
        public object Find(string cnpj)
        {
            IEnumerable<Transport> transport;

            try
            {
                transport = transportService.FindByCnpj(cnpj);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            if (transport.Count() > 0)
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