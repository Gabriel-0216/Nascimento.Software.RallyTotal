using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.RallyTotal.WebApp.Models;
using NascimentoSoftware.RallyTotal.Infraestrutura.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Controllers
{
    public class SaleController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SaleController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var repositorio = await new SaleRepository().GetAll();
            var categories = await new CategoryRepository().GetAll();
            var persons = await new PersonRepository().GetAll();
            var photos = await new PhotoRepository().GetAll();
            var listaCategorias = new List<Category>();
            var listaSales = new List<Sale>();
            var listaPessoas = new List<Person>();
            var listaPhotos = new List<Photo>();

            SaleModelIndex saleModel = new SaleModelIndex();

            foreach(var item in categories)
            {
                listaCategorias.Add(new Category
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    RegisterDate = item.RegisterDate,
                    UpdateDate = item.UpdateDate,
                });
            }
            foreach(var item in repositorio)
            {
                listaSales.Add(new Sale
                {
                    CategoryId = item.CategoryId,
                    PersonID = item.PersonID,
                    SaleId = item.SaleId,
                    Country = item.Country,
                    DescriptionSale = item.DescriptionSale,
                    Photo = item.Photo,
                    UpdateDate = item.UpdateDate,
                    RegisterDate = item.RegisterDate,
                    Price = item.Price,
                    SaleTitle = item.SaleTitle,
                });
            }
            foreach(var item in persons)
            {
                listaPessoas.Add(new Person
                {
                    PersonId = item.PersonId,
                    PersonName = item.PersonName,
                    Country = item.Country,
                    PhoneNumber = item.PhoneNumber,
                });
            }
            foreach(var item in photos)
            {
                listaPhotos.Add(new Photo
                {
                    PhotoId = item.PhotoId,
                    PhotoName = item.PhotoName,
                    Title = item.Title,
                });
            }

            saleModel.Categories = listaCategorias;
            saleModel.People = listaPessoas;
            saleModel.photos = listaPhotos;
            saleModel.Sales = listaSales;

            return View(saleModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await RetunViewModel());
        }

        public async Task<SaleViewModel> RetunViewModel()
        {
            SaleViewModel saleViewModel = new SaleViewModel();
            var categories = await new  CategoryRepository().GetAll();
            var vendors = await new PersonRepository().GetAll();
            var listaCategorias = new List<Category>();
            var listaPessoas = new List<Person>();

           
            foreach (var item in categories)
            {
                listaCategorias.Add(new Category
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName
                });
            }
            foreach (var item in vendors)
            {
                listaPessoas.Add(new Person
                {
                    PersonId = item.PersonId,
                    PersonName = item.PersonName,
                });
            }

            saleViewModel.Categories = listaCategorias;
            saleViewModel.People = listaPessoas;
            return saleViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new SaleRepository();
                var photoRepo = new PhotoRepository();
                var viewModel = new SaleModel()
                {
                    SaleId = model.SaleId,
                    PersonID = model.PersonID,
                    CategoryId = model.CategoryId,
                    DescriptionSale = model.DescriptionSale,
                    Country = model.Country,
                    RegisterDate = model.RegisterDate,
                    UpdateDate = model.UpdateDate,
                    Price = model.Price,
                    SaleTitle = model.SaleTitle,
                    Photo = model.Photo,
                };

                string uniqueFileName = UploadedFile(viewModel);

                var photoInfra = new NascimentoSoftware.RallyTotal.Infraestrutura.Models.Photo
                {
                    PhotoName = uniqueFileName,
                };
                await photoRepo.Add(photoInfra);
                var id = photoRepo.SearchFileName(uniqueFileName);
                var sale = new NascimentoSoftware.RallyTotal.Infraestrutura.Models.Sale()
                {
                    SaleTitle = model.SaleTitle,
                    RegisterDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Country = model.Country,
                    Price = model.Price,
                    PersonID = model.PersonID,
                    CategoryId = model.CategoryId,
                    Photo = id,
                    DescriptionSale = model.DescriptionSale
                };
               await repo.Add(sale);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var saleRepo = await new SaleRepository().GetOne((int)id);
            if (id == null || saleRepo == null)
            {
                return NotFound();
            }
            else
            {
                 var categoryRepo = new CategoryRepository();
                    var category = await categoryRepo.GetOne(saleRepo.CategoryId);
                    var photoRepo = new PhotoRepository();
                    var photo = await photoRepo.GetOne(saleRepo.Photo);
                    var personRepo  = new PersonRepository();
                    var person = await personRepo.GetOne(saleRepo.PersonID);

                    var saleModel = new SaleModelDetails()
                    {
                        SaleId = saleRepo.SaleId,
                        SaleTitle = saleRepo.SaleTitle,
                        DescriptionSale = saleRepo.DescriptionSale,
                        Country = saleRepo.Country,
                        Price = saleRepo.Price,
                        RegisterDate = saleRepo.RegisterDate.ToShortDateString(),
                        UpdateDate = saleRepo.UpdateDate.ToShortDateString(),
                        Photo = photo.PhotoName,
                        PersonSeller = person.PersonName,
                        CategoryName = category.CategoryName,
                    };
                    return View(saleModel);
            }

         
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var saleRepo = new SaleRepository();
            var sale = await saleRepo.GetOne((int)id);
            if (id == null || sale == null)
            {
                return NotFound();
            }
            else
            {
                var saleModel = new SaleModel
                {
                    SaleId = sale.SaleId,
                    SaleTitle = sale.SaleTitle,
                    DescriptionSale = sale.DescriptionSale,
                    RegisterDate = sale.RegisterDate,
                    UpdateDate = sale.UpdateDate,
                    CategoryId = sale.CategoryId,
                    Country = sale.Country,
                    PersonID = sale.PersonID,
                    Price = sale.Price,
                };

                return View(saleModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
                var saleRepo = new SaleRepository();
                await saleRepo.Delete(id);
                return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var saleRepo = new SaleRepository();
            var sale = await saleRepo.GetOne((int)id);
            if (id == null || sale == null)
            {
                return NotFound();
            }
            else
            {
                var saleViewModel = await RetunViewModel();
                saleViewModel.CategoryId = sale.CategoryId;
                saleViewModel.PersonID = sale.PersonID;
                saleViewModel.Price = sale.Price;
                saleViewModel.PersonID = sale.PersonID;
                saleViewModel.SaleId = sale.SaleId;
                saleViewModel.UpdateDate = sale.UpdateDate;
                saleViewModel.RegisterDate = sale.RegisterDate;
                saleViewModel.DescriptionSale = sale.DescriptionSale;
                saleViewModel.Country = sale.Country;
                saleViewModel.SaleTitle = sale.SaleTitle;
                return View(saleViewModel);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaleModel model)
        {
            if (ModelState.IsValid)
            {
                var saleRepo = new SaleRepository();
                var viewModel = new SaleModel()
                {
                    SaleId = model.SaleId,
                    PersonID = model.PersonID,
                    CategoryId = model.CategoryId,
                    DescriptionSale = model.DescriptionSale,
                    Country = model.Country,
                    RegisterDate = model.RegisterDate,
                    UpdateDate = model.UpdateDate,
                    Price = model.Price,
                    SaleTitle = model.SaleTitle,
                    Photo = model.Photo,
                };
                string uniqueFileName = UploadedFile(viewModel);

                var photoRepo = new PhotoRepository();
                var photoInfra = new NascimentoSoftware.RallyTotal.Infraestrutura.Models.Photo
                {
                    PhotoName = uniqueFileName,
                };
                await photoRepo.Add(photoInfra);
                var id = photoRepo.SearchFileName(uniqueFileName);
                var sale = new NascimentoSoftware.RallyTotal.Infraestrutura.Models.Sale()
                {
                    SaleId = model.SaleId,
                    SaleTitle = model.SaleTitle,
                    RegisterDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Country = model.Country,
                    Price = model.Price,
                    PersonID = model.PersonID,
                    CategoryId = model.CategoryId,
                    Photo = id,
                    DescriptionSale = model.DescriptionSale
                };
                await saleRepo.Update(sale);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(SaleModel model)
        {
            string uniqueFileName = null;
            if(model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        
    }
}