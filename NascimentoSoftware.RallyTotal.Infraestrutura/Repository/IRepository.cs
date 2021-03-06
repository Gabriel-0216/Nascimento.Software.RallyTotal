using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NascimentoSoftware.RallyTotal.Infraestrutura.Repository
{
    public interface IRepository<T>
    {
        Task<int> Add(T objeto);
        Task<int> Delete(int id);
        Task<int> Update(T objeto);

        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(int id);
        bool Exists(int id);


    }
}
