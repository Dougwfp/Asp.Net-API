using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using InternoApi.Data;
using InternoApi.Models;
using InternoApi.Dao;
using InternoApi.Interfaces;

namespace InternoApi.Dao
{
    public class Transport_cDAO : ITransport_cDAO<Transport_c>
    {
        public PortalContext db { get; set; }

        public Transport_cDAO()
        {
            this.db = new PortalContext();
        }

        public IQueryable<Transport_c> Index()
        {
            try
            {
                return db.Transports_c.OrderBy(transp => transp.TRANSP_NOME);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Transport_c> IndexByRazao(string razao)
        {
            IQueryable<Transport_c> transports;

            try
            {
                transports = from transp in db.Transports_c
                             where transp.TRANSP_NOME.Contains(razao)
                             select transp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transports.OrderBy(transp => transp.TRANSP_NOME);
        }

        public IQueryable<Transport_c> IndexByTranspId(int transp_id)
        {
            IQueryable<Transport_c> transports;

            try
            {
                transports = db.Transports_c.Where(transp => transp.TRANSP_ID == transp_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transports.OrderBy(transp => transp.TRANSP_NOME);
        }

        public Transport_c Find(int id)
        {
            try
            {
                return db.Transports_c.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Transport_c FindByRazao(string razao)
        {
            IQueryable<Transport_c> transports;

            try
            {
                transports = db.Transports_c.Where(transp => (transp.TRANSP_NOME == razao));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transports.FirstOrDefault();
        }

        public object Create(Transport_c transport)
        {
            try
            {
                db.Transports_c.Add(transport);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return transport;
        }

        public object Update(Transport_c transport)
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

        public void Delete(Transport_c transport)
        {
            try
            {
                db.Transports_c.Remove(transport);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int transp_id)
        {
            IQueryable<Transport_c> transports;

            try
            {
                transports = db.Transports_c.Where(transp => transp.TRANSP_ID == transp_id);

                db.Transports_c.RemoveRange(transports);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}