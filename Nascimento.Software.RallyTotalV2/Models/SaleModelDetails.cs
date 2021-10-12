using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models
{
    public class SaleModelDetails
    {
        public int SaleId { get; set; }
        public string RegisterDate { get; set; }
        public string UpdateDate { get; set; }
        public string SaleTitle { get; set; }
        public string Country { get; set; }
        public double Price { get; set; }
        public string PersonSeller { get; set; }
        public string CategoryName { get; set; }  
        public string Photo { get; set; }
        public string DescriptionSale { get; set; }
    }
}
