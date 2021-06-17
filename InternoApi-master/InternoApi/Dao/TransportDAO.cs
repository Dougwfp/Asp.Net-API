using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternoApi.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using InternoApi.Data;
using InternoApi.Dao;
using InternoApi.Interfaces;

namespace InternoApi.Dao
{
    public class TransportDAO : ITransportDAO<Transport>
    {
        public PortalContext db { get; set; }

        private Transport_cDAO transport_cDao = new Transport_cDAO();

        public TransportDAO()
        {
            this.db = new PortalContext();
        }

        public object Create(Transport transport)
        {
            Transport_c transport_c = new Transport_c(transport.TRANSP_ID, transport.TRANSP_NOME);

            try
            {
                //transport_cDao.Create(transport_c);

                db.Transports.Add(transport);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transport;
        }

        public void Delete(Transport transport)
        {
            try
            {
                //transport_cDao.Delete(transport.TRANSP_ID);

                db.Transports.Remove(transport);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public Transport Find(int id)
        {
            try
            {
                return db.Transports.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Transport FindByCnpj(string transp_cnpj)
        {
            Transport transport;
            try
            {
                transport = db.Transports.FirstOrDefault(transp => transp.TRANSP_CNPJ == transp_cnpj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transport;
        }

        public IQueryable<Transport> IndexByRazao(string razao)
        {
            IQueryable<Transport> transports;

            try
            {
                transports = from transp in db.Transports
                                                   where transp.TRANSP_NOME.Contains(razao)
                                                   select transp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transports.OrderBy(transp => transp.TRANSP_NOME);
        }

        public IQueryable<Transport> Index()
        {
            try
            {
                return db.Transports.OrderBy(transp => transp.TRANSP_NOME);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Update(Transport transport)
        {
            try
            {
                db.Entry(transport).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transport;
        }
    }
}