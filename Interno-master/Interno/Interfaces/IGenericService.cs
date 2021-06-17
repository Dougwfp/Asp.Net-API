using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interno.Interfaces
{
    interface IGenericService<T>
    {
        IEnumerable<T> Index();

        T Find(int id);

        T Create(T transport);

        T Update(T transport);

        T Delete(T transport);
    }
}