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
        public ActionResult Index()
        {
            int? korisnikid = HttpContext.Session.GetInt32("korisnikid"); //vraca null ako ne postoji, zato ?
            int? administratorid = HttpContext.Session.GetInt32("administratorid");
            if (korisnikid != null)
            {
                ViewBag.IsLoggedInKorisnik = true;
            }
            if(administratorid!=null)
                ViewBag.IsLoggedInAdministrator = true;
            return View(uow.Test.GetAll());
        }

        // GET: TestController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.IsLoggedInAdministrator = true;
            return View();
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
        public ActionResult Create([FromForm]ViewModel test)
        {
            ViewBag.IsLoggedInAdministrator = true;
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
        public ActionResult Delete(int id)
        {
            ViewBag.IsLoggedInAdministrator = true;
            return View();
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
