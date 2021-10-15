using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.RallyTotal.WebApp.Models.Blog;
using Nascimento.Software.RallyTotal.WebApp.Models.Blog.ViewModel;
using NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Controllers
{
    public class PostController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var postRepo = await new PostRepository().GetAll();
                var listaPostViewModel = new List<PostViewModel>();
                var author = new AutorRepository();
                foreach (var item in postRepo)
                {
                    var autor = await author.GetOne(item.AutorId);
                    listaPostViewModel.Add(new PostViewModel()
                    {
                        Id = item.Id,
                        Assunto = item.Assunto,
                        Texto = item.Texto,
                        AutorNome = autor.Nome,
                    });
                }
                return View(listaPostViewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var authorRepo = await new AutorRepository().GetAll();
            var postRepo = await new PostRepository().GetAll();

            var postVM = new PostVM_Create();
            var listaAuthores = new List<Autor>();

            foreach(var item in authorRepo)
            {
                listaAuthores.Add(new Autor
                {
                    Id = item.Id,
                    Nome = item.Nome,
                });
            }

            postVM.authors = listaAuthores;


            return View(postVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostVM_Create model)
        {
            if (ModelState.IsValid)
            {
                var post = new NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Models.Post()
                {
                    AutorId = model.AutorId,
                    Texto = model.Texto,
                    Assunto = model.Assunto,
                };

                var postRepo = await new PostRepository().Add(post);
                if(postRepo >= 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception("Ocorreu um erro ao tentar adicionar esse registro.");
                }
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id==null || await new PostRepository().GetOne((int)id)== null)
            {
                return NotFound();
            }
            else
            {
                var post = await new PostRepository().GetOne((int)id);
                var autor = await new AutorRepository().GetOne(post.AutorId);
                var postvm = new PostViewModel()
                {
                    Id = (int)id,
                    Assunto = post.Assunto,
                    Texto = post.Texto,
                    AutorNome = autor.Nome,
                };
                return View(postvm);
            }
        }
    }
}
