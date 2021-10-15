using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int AutorId { get; set; }
        public string Assunto { get; set; }
        public string Texto { get; set; }

    }
}
