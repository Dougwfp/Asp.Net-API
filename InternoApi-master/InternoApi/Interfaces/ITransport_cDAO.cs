using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternoApi.Dao;
using InternoApi.Interfaces;
using InternoApi.Models;
using InternoApi.Data;
using InternoApi.Controllers;

namespace InternoApi.Interfaces
{
    interface ITransport_cDAO<T> : IGenericDAO<T>
    {
        IQueryable<T> IndexByRazao(string razao);

        T FindByRazao(string razao);

        IQueryable<T> IndexByTranspId(int transp_id);

        void Delete(int transp_id);
    }
}
