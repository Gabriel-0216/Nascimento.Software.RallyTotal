using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models.Blog
{
    public class Post
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="É necessário digitar um assunto do post.")]
        [StringLength(255, ErrorMessage ="O limite máximo de caracteres do assunto da postagem são 255 caracteres")]
        public string Assunto { get; set; }

        [Required(ErrorMessage ="É necessário informar o texto da postagem")]
        public string Texto { get; set; }
        [Required(ErrorMessage ="É necessário informar o ID do autor")]
        public int AutorId { get; set; }

    }
}
