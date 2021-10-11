using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NascimentoSoftware.RallyTotal.Infraestrutura.Repository
{
    public interface IRepository<T>
    {
        int Add(T objeto);
        int Delete(int id);
        int Update(T objeto);
        List<T> GetAll();
        T GetOne(int id);
        bool Exists(int id);


    }
}
