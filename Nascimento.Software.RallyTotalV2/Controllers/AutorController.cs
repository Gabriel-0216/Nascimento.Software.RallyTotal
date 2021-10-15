using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.RallyTotal.WebApp.Models.Blog;
using NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Controllers
{
    public class AutorController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var autorRepo = new AutorRepository();
            return View(await autorRepo.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Autor model)
        {
            if (ModelState.IsValid)
            {
                var autorRepo = new AutorRepository();
                var autorModel = new NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Models.Autor()
                {
                    Nome = model.Nome,
                };
                await autorRepo.Add(autorModel);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var autorModel = await GetAutor((int)id);
                return View(autorModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var autorRepo = await new AutorRepository().Delete((int)id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var autorModel = await GetAutor((int)id);
                return View(autorModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Autor model)
        {
            if (ModelState.IsValid)
            {
                var autor = new NascimentoSoftware.RallyTotal.Infraestrutura.Blog.Infraestrutura.Models.Autor()
                {
                    Nome = model.Nome,
                    Id = model.Id,
                };
                var autorRepo = new AutorRepository();
                await autorRepo.Update(autor);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        public async Task<Autor> GetAutor(int id)
        {
            var autorRepo = new AutorRepository();
            var autor = await autorRepo.GetOne(id);
            var autorModel = new Autor
            {
                Id = autor.Id,
                Nome = autor.Nome,
            };
            return autorModel;
        }
    }
}
