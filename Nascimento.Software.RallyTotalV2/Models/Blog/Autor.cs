using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models.Blog
{
    public class Autor
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="É necessário informar o nome do autor")]
        public string Nome { get; set; }
    }
}
