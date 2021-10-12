using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.RallyTotal.WebApp.Models;
using NascimentoSoftware.RallyTotal.Infraestrutura.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Controllers
{
    public class PersonController : Controller
    {

        public IActionResult Index()
        {
            var repo = new PersonRepository();
            var lista = repo.GetAll();
            var personList = new List<Person>();
            foreach (var item in lista)
            {
                personList.Add(new Person
                {
                    PersonName = item.PersonName,
                    PersonId = item.PersonId,
                    Country = item.Country,
                    PhoneNumber = item.PhoneNumber,
                });
            }
            return View(personList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person model)
        {
            if (ModelState.IsValid)
            {
                var repo = new PersonRepository();
                var person = new NascimentoSoftware.RallyTotal.Infraestrutura.Models.Person
                {
                    PersonName = model.PersonName,
                    PhoneNumber = model.PhoneNumber,
                    Country = model.Country,

                };
                repo.Add(person);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                
                var repo = new PersonRepository().GetOne((int)id);
                if (repo == null)
                {
                    return NotFound();
                }
                else
                {
                    var person = new Person
                    {
                        PersonId = repo.PersonId,
                        PersonName = repo.PersonName,
                        PhoneNumber = repo.PhoneNumber,
                        Country = repo.Country,
                    };
                    return View(person);
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(Person model)
        {
            if (ModelState.IsValid)
            {
                var repo = new PersonRepository();
                var person = new NascimentoSoftware.RallyTotal.Infraestrutura.Models.Person
                {
                    PersonId = model.PersonId,
                    PersonName = model.PersonName,
                    PhoneNumber = model.PhoneNumber,
                    Country = model.Country,
                };
                repo.Update(person);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var saleRepo = new SaleRepository();
            if (id == null)
            {
                return NotFound();
            }
            else if(saleRepo.SellerExists((int)id) >= 1)
            {
             
                return NotFound();
            }
            else
            {
                var repo = new PersonRepository().GetOne((int)id);
                if (repo == null)
                {
                    return NotFound();
                }
                else
                {
                    var person = new Person
                    {
                        PersonId = repo.PersonId,
                        PersonName = repo.PersonName,
                        PhoneNumber = repo.PhoneNumber,
                        Country = repo.Country,
                    };
                    return View(person);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var repository = new PersonRepository();
            repository.Delete(id);
            return RedirectToAction(nameof(Index));
           
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var repo = new PersonRepository().GetOne((int)id);
                var person = new Person
                {
                    PersonId = repo.PersonId,
                    PersonName = repo.PersonName,
                    PhoneNumber = repo.PhoneNumber,
                    Country = repo.Country,
                };
                return View(person);
            }
        }

    }
            
            


}
