using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.RallyTotal.WebApp.Models;
using NascimentoSoftware.RallyTotal.Infraestrutura.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Controllers
{
    public class CategoryController : Controller
    {

        public IActionResult Index()
        {
            var repositorio = new CategoryRepository();
            var lista = repositorio.GetAll();
            var Category = new List<Category>();
            foreach (var item in lista)
            {
                Category.Add(new Category
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    RegisterDate = item.RegisterDate,
                    UpdateDate = item.UpdateDate
                });
            }
            return View(Category.OrderBy(m=> m.CategoryId));

        }

   
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var repositorio = new CategoryRepository();
                var categoria = new NascimentoSoftware.RallyTotal.Infraestrutura.Models.Category
                {
                    CategoryName = category.CategoryName,
                    RegisterDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                };
                repositorio.Add(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
       
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var categoria = new CategoryRepository().GetOne((int)id);
                if (categoria == null)
                {
                    return NotFound();
                }
                else
                {
                    var cate = new Category
                    {
                        CategoryId = categoria.CategoryId,
                        CategoryName = categoria.CategoryName,
                        RegisterDate = categoria.RegisterDate,
                        UpdateDate = categoria.UpdateDate,
                    };
                    return View(cate);
                }
            }
        }

        [HttpPost]
     
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category categoria)
        {
            if (ModelState.IsValid)
            {
                var repositorio = new CategoryRepository();
                var cate = new NascimentoSoftware.RallyTotal.Infraestrutura.Models.Category()
                {
                    CategoryId = categoria.CategoryId,
                    CategoryName = categoria.CategoryName,
                    UpdateDate = DateTime.Now
                };
                repositorio.Update(cate);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var categoria = new CategoryRepository().GetOne((int)id);
            if (categoria == null)
            {
                return NotFound();
            }
            else
            {
                var cate = new Category
                {
                    CategoryId = categoria.CategoryId,
                    CategoryName = categoria.CategoryName,
                    RegisterDate = categoria.RegisterDate,
                    UpdateDate = categoria.UpdateDate,
                };
                return View(cate);
            }
        }


    
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var repo = new CategoryRepository().GetOne((int)id);
                if (repo == null)
                {
                    return NotFound();
                }
                else
                {
                    var category = new Category()
                    {
                        CategoryId = repo.CategoryId,
                        CategoryName = repo.CategoryName,
                        RegisterDate = repo.RegisterDate,
                        UpdateDate = repo.UpdateDate,
                    };
                    return View(category);
                }
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = new CategoryRepository().GetOne((int)id);
            var repo = new CategoryRepository();
            repo.Delete(category.CategoryId);
            return RedirectToAction(nameof(Index));
        }
    }
}
