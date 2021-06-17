using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternoApi.Dao;
using InternoApi.Data;
using InternoApi.Models;
using InternoApi.Interfaces;
using InternoApi.Controllers;

namespace InternoApi.Interfaces
{
    interface ITransportDAO<T> : IGenericDAO<T>
    {
        IQueryable<T> IndexByRazao(string razao);

        T FindByCnpj(string transp_cnpj);
    }
}
