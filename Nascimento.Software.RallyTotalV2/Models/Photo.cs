using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models
{
    public class Photo
    {  
        public int PhotoId { get; set; }
        public string Title { get; set; }
        public string PhotoName { get; set; }
    }
}
