using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interno.Interfaces
{
    interface ICliente_cService<T> : IGenericService<T>
    {
        IEnumerable<T> IndexByRazao(string razao);

        T FindByRazao(string razao);

        IEnumerable<T> IndexByClienteId(int cliente_id); //me procure na api

        void Delete(int cliente_id);
    }
}