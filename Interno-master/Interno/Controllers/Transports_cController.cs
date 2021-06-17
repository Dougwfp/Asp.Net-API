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
    public class Transports_cController : Controller
    {

        private ITransport_cService<Transport_c> transport_cService;
        private ITransportService<Transport> transportService;

        public Transports_cController()
        {
            this.transport_cService = new Transport_cService();
            this.transportService = new TransportService();
        }
        // GET: Transports_c
        public ActionResult Index(int transp_id = 0, string searchKey = null)
        {
            Transport transport;
            Transport_c transport_c;
            IEnumerable<Transport_c> transports;

            try
            {
                transport = transportService.Find(transp_id);
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

            ViewBag.Transp_nome = transport.TRANSP_NOME;
            ViewBag.Transp_cnpj = transport.TRANSP_CNPJ;
            ViewBag.transp_id = transport.TRANSP_ID;

            if (transp_id != 0)
            {
                try
                {
                    transport_c = transport_cService.FindByRazao(transport.TRANSP_NOME);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                if (transport_c == null)
                {
                    transport_c = new Transport_c(transp_id, transport.TRANSP_NOME);

                    try
                    {
                        transport_cService.Create(transport_c);
                    }
                    catch (Exception ex)
                    {
                        TempData["Message"] = ex.Message;
                        return RedirectToAction("Error");
                    }
                }

                try
                {
                    transports = transport_cService.IndexByTranspId(transp_id);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                return View(transports);
            }

            if (searchKey != null)
            {
                try
                {
                    transports = transport_cService.IndexByRazao(searchKey);
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Error");
                }

                if (transports.Count() > 0)
                {
                    return View(transports);
                }
            }

            try
            {
                return View(transport_cService.Index());
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public ActionResult Create(int transp_id = 0)
        {
            if (transp_id == 0)
            {
                return HttpNotFound();
            }

            ViewBag.transp_id = transp_id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Transport_c transport, int transp_id)
        {
            if (!ModelState.IsValid)
            {
                return View(transport);
            }

            transport.TRANSP_ID = transp_id;
            transport.TRANSP_NOME = transport.TRANSP_NOME.ToUpper();

            try
            {
                transport_cService.Create(transport);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index", "Transports_c", new { transp_id });
        }

        //[HttpDelete]
        [HttpPost]
        [Route("Transports_c/Delete/{id}")]
        public ActionResult Delete(int id, int? transp_id)
        {
            Transport_c transport_c;

                transport_c = transport_cService.Find(id);


            if (transport_c == null)
            {
                return HttpNotFound();
            }

            try
            {
                transport_cService.Delete(transport_c);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Error");
            }

            return RedirectToAction("Index", new { transp_id = transp_id });
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
