using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Repository
{
    public interface IRepository<T>
    {
        string GetConnection();
        Task<int> Add(T objeto);
        Task<int> Update(T objeto);
        Task<int> Delete(int id);

        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(int id);
    }
}
