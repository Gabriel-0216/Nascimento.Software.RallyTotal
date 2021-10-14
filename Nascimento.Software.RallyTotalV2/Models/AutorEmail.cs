using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models
{
    public class AutorEmail
    {
        [Required(ErrorMessage ="É necessário informar o seu nome.")]
        [Display(Description = "Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "É necessário informar o seu email.")]
        [Display(Description = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Você tem que informar um assunto.")]
        [Display(Description ="Assunto")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Você tem que escrever algo no texto..")]
        [Display(Description = "Texto")]
        public string Texto { get; set; }
           


    }
}
