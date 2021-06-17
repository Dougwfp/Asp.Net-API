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
    public class TransportsController : ApiController
    {
        private ITransportDAO<Transport> transportDao;
        private ITransport_cDAO<Transport_c> transport_cDao;

        public TransportsController()
        {
            this.transportDao = new TransportDAO();
            this.transport_cDao = new Transport_cDAO();
        }

        [Route("Api/Transports")]
        public object GetTransports()
        {
            try
            {
                return transportDao.Index();
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }
        }

        [Route("Api/Transports/{id}")]
        [ResponseType(typeof(Transport))]
        public object GetTransport(int id)
        {
            Transport transport;

            try
            {
                transport = transportDao.Find(id);

                if (transport == null)
                {
                    return new { Message = "Not Found" };
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            return transport;

            //cria um apelido -> comentado, pois, faz parte da 'regra de negócio'. Cabe ao app que está consumindo a api chamar este método
            //var transport_c = transport_cDao.FindByRazao(transport.TRANSP_NOME);

            //if (transport_c == null)
            //{
            //    transport_cDao.Create(new Transport_c(transport.TRANSP_ID, transport.TRANSP_NOME));
            //}
        }

        [Route("Api/Transports/Cnpj/{cnpj}")]
        [ResponseType(typeof(Transport))]
        public object GetTransportByCnpj(string cnpj)
        {
            Transport transport;

            try
            {
                transport = transportDao.FindByCnpj(cnpj);

                if (transport == null)
                {
                    return new { Message = "Not Found" };
                }

            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            return transport;
        }

        [Route("Api/Transports/Razao/{razao}")]
        [ResponseType(typeof(Transport))]
        public object GetTransportByRazao(string razao)
        {
            IQueryable<Transport> transport;

            try
            {
                transport = transportDao.IndexByRazao(razao);

                if (transport == null)
                {
                    return new { Message = "Not Found" };
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            return transport;
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        [Route("Api/Transports/{id}")]
        public object PutTransport(int id, Transport transport)
        {
            if (!ModelState.IsValid)
            {
                return new { Message = "Bad Request" };
            }

            if (id != transport.TRANSP_ID)
            {
                return new { Message = "Bad Request" };
            }

            try
            {
                transportDao.Update(transport);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TransportExists(id))
                {
                    return new { Message = "Not Found" };
                }
                else
                {
                    return new { ex.Message };
                }
            }

            return transport;
        }

        [HttpPost]
        [ResponseType(typeof(Transport))]
        [Route("Api/Transports")]
        public object PostTransport(Transport transport)
        {
            if (!ModelState.IsValid)
            {
                return new { Message = "Bad Request" };
            }

            try
            {
                transportDao.Create(transport);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            return transport;
        }

        [HttpDelete]
        [Route("Api/Transports/{id}")]
        [ResponseType(typeof(Transport))]
        public object DeleteTransport(int id)
        {
            Transport transport;

            try
            {
                transport = transportDao.Find(id);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { ex.Message });
            }

            if (transport == null)
            {
                return new { Message = "Not Found" };
            }

            try
            {
                transportDao.Delete(transport);
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }

            return transport;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    transportDao.db.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            base.Dispose(disposing);
        }

        private bool TransportExists(int id)
        {
            return transportDao.Index().Count(e => e.TRANSP_ID == id) > 0;
        }
    }
}