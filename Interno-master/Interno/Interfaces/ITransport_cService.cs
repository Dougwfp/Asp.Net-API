using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interno.Interfaces
{
    interface ITransport_cService<T> : IGenericService<T>
    {
        IEnumerable<T> IndexByRazao(string razao);

        T FindByRazao(string razao);

        IEnumerable<T> IndexByTranspId(int transp_id); //me procure na api

        void Delete(int transp_id);
    }
}