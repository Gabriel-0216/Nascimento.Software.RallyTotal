using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models
{
    public class SaleModelIndex
    {
  
        public List<Sale> Sales { get; set; }
        public List<Category> Categories { get; set; }
        public List<Person> People { get; set; }
        public List<Photo> photos { get; set; }

    }
}
