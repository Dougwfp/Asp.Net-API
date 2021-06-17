using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternoApi.Dao;
using InternoApi.Data;
using InternoApi.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using InternoApi.Interfaces;

namespace InternoApi.Dao
{
    public class TesteDAO : IGenericDAO<Transport_c>
    {
        public PortalContext db { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object fazAlgo()
        {
            return new { name = "gdfgd" };
        }

        public object Create(Transport_c transport)
        {
            throw new NotImplementedException();
        }

        public void Delete(Transport_c transport)
        {
            throw new NotImplementedException();
        }

        public Transport_c Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Transport_c> Index()
        {
            Transport_c transport = new Transport_c();
            transport.TRANSP_ID = 888888;
            transport.TRANSP_NOME = "Teste";

            
            List<Transport_c> list = new List<Transport_c>();
            list.Add(transport);

            return list.AsQueryable();
        }

        public IQueryable<Transport_c> IndexByRazao(string razao)
        {
            throw new NotImplementedException();
        }

        public object Update(Transport_c transport)
        {
            throw new NotImplementedException();
        }
    }
}