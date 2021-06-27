﻿using System;
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
        [LoggedInKorisnik]
        public ActionResult PrikaziTestKorsnik([FromRoute(Name = "id")] int kursId,TestsViewModel tvm)
        {
            ViewBag.IsLoggedInKorisnik = true;
            //za ovaj kurs samo da se prikazu testovi
           // int? kursid = HttpContext.Session.GetInt32("kursid"); 
            tvm.Testovi = uow.Test.GetAll().Where(t => t.KursId == kursId).ToList();
            return View("TestKorisnik",tvm);
        }

        // GET: TestController/Details/5
        [LoggedInAdministrator]
        public ActionResult Details([FromRoute(Name = "id")] int id, TestsViewModel tvm)
        {
            ViewBag.IsLoggedInAdministrator = true;
            tvm.TestId = id;
            List<Pitanje> pitanja = uow.Pitanje.GetAll().Where(p => p.TestId == tvm.TestId).ToList();
            List<Checkbox> checkboxes = pitanja.OfType<Checkbox>().ToList();
            List<Dopuna> dopune = pitanja.OfType<Dopuna>().ToList();
            tvm.Checkboxes = checkboxes;
            tvm.Dopune = dopune;
            return View(tvm);
        }
        [LoggedInKorisnik]
        public ActionResult PitanjaKorisnik([FromRoute(Name = "id")] int id, TestsViewModel tvm)
        {
            ViewBag.IsLoggedInKorisnik = true;
            tvm.TestId = id;
            List<Pitanje> pitanja = uow.Pitanje.GetAll().Where(p => p.TestId == tvm.TestId).ToList();
            List<Checkbox> checkboxes = pitanja.OfType<Checkbox>().ToList();
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
        public ActionResult Delete([FromRoute (Name ="id")]int id, TestsViewModel tvm)
        {
            ViewBag.IsLoggedInAdministrator = true;
            Test model = uow.Test.FindById(new Test { TestId = id });
            model.Pitanja = uow.Pitanje.GetAll().Where(p => p.TestId == model.TestId).ToList();
            uow.Test.Delete(model);
            uow.Commit();
            return RedirectToAction("Index","Test");
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

        [LoggedInAdministrator]

        public ActionResult PogledajPitanjeDopuna([FromRoute(Name = "id")] int id)
        {
            ViewBag.IsLoggedInAdministrator = true;
            Dopuna model = (Dopuna)uow.Pitanje.FindById(new Pitanje { PitanjeId = id }); //ne znam da li moze ovako
            return View("PitanjeDopuna", model);
        }

        public ActionResult PogledajPitanjeCheckbox([FromRoute(Name = "id")] int id)
        {
            ViewBag.IsLoggedInAdministrator = true;
            Checkbox model = (Checkbox)uow.Pitanje.FindById(new Pitanje { PitanjeId = id }); //ne znam da li moze ovako
            return View("PitanjeCheckbox", model);
        }

    }
}
