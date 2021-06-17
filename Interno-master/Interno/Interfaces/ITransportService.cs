using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interno.Interfaces
{
    interface ITransportService<T> : IGenericService<T>
    {
        IEnumerable<T> IndexByRazao(string razao);

        IEnumerable<T> FindByCnpj(string transp_cnpj);
    }
}