using InternoApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternoApi.Interfaces
{
    public interface IGenericDAO<T>
    {
        PortalContext db { get; set; }

        IQueryable<T> Index();

        T Find(int id);

        object Create(T transport);

        object Update(T transport);

        void Delete(T transport);
    }
}
