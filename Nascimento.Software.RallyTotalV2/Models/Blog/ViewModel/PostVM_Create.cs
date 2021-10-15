using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models.Blog.ViewModel
{
    public class PostVM_Create
    {
        public int Id { get; set; }
        public int AutorId { get; set; }
        public string Assunto { get; set; }
        public string Texto { get; set; }
        public List<Autor> authors { get; set; }
    }
}
