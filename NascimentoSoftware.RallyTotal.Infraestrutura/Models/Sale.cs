using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NascimentoSoftware.RallyTotal.Infraestrutura.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public string SaleTitle { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Country { get; set; }
        public double Price { get; set; }
        public int PersonID { get; set; }
        public int CategoryId { get; set; }
        public int Photo { get; set; }
        public string DescriptionSale { get; set; }


    }
}
