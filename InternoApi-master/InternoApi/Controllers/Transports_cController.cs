using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using InternoApi.Data;
using InternoApi.Dao;
using InternoApi.Models;
using Newtonsoft.Json;
using InternoApi.Interfaces;

namespace InternoApi.Controllers
{
    public class Transports_cController : ApiController
    {
        private ITransport_cDAO<Transport_c> transport_cDao;

        public Transports_cController()
        {
            this.transport_cDao = new Transport_cDAO();
        }

        [Route("Api/Transports_c")]
        public object GetTransports_c()
        {
            try
            {
                return transport_cDao.Index();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }
        }

        [Route("Api/Transports_c/Razao/{razao}")]
        [ResponseType(typeof(Transport_c))]
        public object GetTransports_c(string razao)
        {
            try
            {
                return transport_cDao.IndexByRazao(razao);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }
        }

        [Route("Api/Transports/{transp_id}/Transports_c")]
        [ResponseType(typeof(Transport_c))]
        public object GetTransports_c(int transp_id)
        {
            try
            {
                return transport_cDao.IndexByTranspId(transp_id);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }
        }

        [Route("Api/Transports_c/{id}")]
        [ResponseType(typeof(Transport_c))]
        public object GetTransport_c(int id)
        {
            Transport_c transport_c;

            try
            {
                transport_c = transport_cDao.Find(id);

                if (transport_c == null)
                {
                    return new { Message = "Not Found" };
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
                throw;
            }

            return transport_c;   
        }

        [HttpGet]
        [Route("Api/Transports_c/Razao/Find/{razao}")]
        [ResponseType(typeof(Transport_c))]
        public object GetTransport_c(string razao)
        {
            Transport_c transport_c;

            try
            {
                transport_c = transport_cDao.FindByRazao(razao);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            if (transport_c == null)
            {
                return new { Message = "Not Found" };
            }

            return transport_c;
        }

        [HttpPut]
        [Route("Api/Transports_c/{id}")]
        [ResponseType(typeof(void))]
        public object PutTransport_c(int id, Transport_c transport_c)
        {
            if (!ModelState.IsValid)
            {
                return new { Message = "Bad Request" };
            }

            if (id != transport_c.TRANSP_C_ID)
            {
                return new { Message = "Bad Request" };
            }

            try
            {
                transport_cDao.Update(transport_c);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!Transport_cExists(id))
                {
                    return JsonConvert.SerializeObject(new { Message = "Not Found" });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { ex.Message });
                }
            }

            return transport_c;
        }

        [HttpPost]
        [Route("Api/Transports_c/")]
        [ResponseType(typeof(Transport_c))]
        public object PostTransport_c(Transport_c transport_c)
        {
            if (!ModelState.IsValid)
            {
                return new { Message = "Bad request" };
            }

            try
            {
                transport_cDao.Create(transport_c);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            return transport_c;
        }

        [HttpDelete]
        [Route("Api/Transports_c/{id}")]
        [ResponseType(typeof(Transport_c))]
        public object DeleteTransport_c(int id)
        {
            Transport_c transport_c;

            try
            {
                transport_c = transport_cDao.Find(id);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            if (transport_c == null)
            {
                return new { Message = "Not Found" };
            }

            try
            {
                transport_cDao.Delete(transport_c);
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }

            return transport_c;
        }

        [HttpDelete]
        [Route("Api/Transports/{transp_id}/Transports_c")]
        [ResponseType(typeof(Transport_c))]
        public object DeleteTransport_cByTranspId(int transp_id)
        {
            IQueryable<Transport_c> transport_c;

            try
            {
                transport_c = transport_cDao.IndexByTranspId(transp_id);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            if (transport_c.Count() == 0)
            {
                return new { Message = "Not Found" };
            }

            try
            {
                transport_cDao.Delete(transp_id);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }
            
            return transport_c;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    transport_cDao.db.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            base.Dispose(disposing);
        }

        private bool Transport_cExists(int id)
        {
            return transport_cDao.Index().Count(e => e.TRANSP_C_ID == id) > 0;
        }
    }
}