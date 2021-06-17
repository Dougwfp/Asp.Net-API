using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interno.Interfaces
{
    interface IClienteService<T> : IGenericService<T>
    {
        IEnumerable<T> IndexByRazao(string razao);

        IEnumerable<T> FindByCnpj(string cliente_cnpj);
    }
}