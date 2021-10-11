using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models
{
    public class Category
    {

        [Display(Name = "Id")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "É necessário informar o nome da categoria")]
        [StringLength(255, ErrorMessage = "O limite máximo de caracteres é: 255")]
        [Display(Name = "Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Register Date")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }



    }
}
