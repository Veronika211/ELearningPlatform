using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Filters;
using WebApp.Models;

namespace WebApp.Controllers
{
    [LoggedInBoth] //ovo znaci valjda da svemu moze da pristupi korisnik samo koji je prijavljen
    public class TestController : Controller
    {
        private readonly IUnitOfWork uow;
        // GET: TestController
        public TestController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public ActionResult Index(TestsViewModel tvm)
        {
            int? korisnikid = HttpContext.Session.GetInt32("korisnikid"); //vraca null ako ne postoji, zato ?
            int? administratorid = HttpContext.Session.GetInt32("administratorid");
            if (korisnikid != null)
            {
                ViewBag.IsLoggedInKorisnik = true;
            }
            if(administratorid!=null)
                ViewBag.IsLoggedInAdministrator = true;
            tvm.Testovi = uow.Test.GetAll(); //definisala sam sta su testovi
            return View(tvm);
        }

        // GET: TestController/Details/5
        public ActionResult Details([FromRoute(Name = "id")] int id, TestsViewModel tvm)
        {
             ViewBag.IsLoggedInAdministrator = true;
            tvm.TestId = id;
            List<Pitanje> pitanja = uow.Pitanje.GetAll().Where(p => p.TestId == tvm.TestId).ToList();
;           List<Checkbox> checkboxes = pitanja.OfType<Checkbox>().ToList();
            List<Dopuna> dopune = pitanja.OfType<Dopuna>().ToList();
            tvm.Checkboxes = checkboxes;
            tvm.Dopune = dopune;
            return View(tvm);
        }

            // GET: TestController/Create
            public ActionResult Create(int id)
        {
            List<SelectListItem> kursevi = new List<SelectListItem>();
            foreach(Kurs k in uow.Kurs.GetAll())
            {
                kursevi.Add(new SelectListItem { Value = k.KursId.ToString(), Text = k.NazivKursa });
            }
            ViewBag.IsLoggedInAdministrator = true;
            ViewData["Kursevi"] = kursevi;
            ViewModel model = new ViewModel { Kursevi = kursevi };
            return View(model);
        }

        // POST: TestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggedInAdministrator]
        public ActionResult Create([FromForm]ViewModel test)
        {
            ViewBag.IsLoggedInAdministrator = true;
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            try
            {
                Test t = new Test
                {
                    KursId = test.KursId,
                    TestId = test.TestId,
                    Nivo = test.Nivo
                };
                uow.Test.Add(t);
                uow.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: TestController/Edit/5
        [LoggedInAdministrator]
        public ActionResult Edit(int id)
        {
            ViewBag.IsLoggedInAdministrator = true;
            return View();
        }

        // POST: TestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            ViewBag.IsLoggedInAdministrator = true;
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestController/Delete/5
        public ActionResult Delete([FromRoute (Name ="id")]int id)
        {
            ViewBag.IsLoggedInAdministrator = true;
            Test model = uow.Test.FindById(new Test { TestId = id });
            uow.Test.Delete(model);
            uow.Commit();
            return View();
        }

        public ActionResult IzbrisiPitanje([FromRoute(Name = "id")] int id, TestsViewModel tvm)
        {
            ViewBag.IsLoggedInAdministrator = true;
            Pitanje model = uow.Pitanje.FindById(new Pitanje { PitanjeId = id });
            uow.Pitanje.Delete(model);
            uow.Commit();
            return RedirectToAction("Index", "Test");
        }



        // POST: TestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            ViewBag.IsLoggedInAdministrator = true;
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        

       
    }
}
