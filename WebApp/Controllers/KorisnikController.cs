using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
    [LoggedInKorisnik]
    public class KorisnikController : Controller
    {
        private readonly IUnitOfWork uow;
        public KorisnikController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        // GET: KorisnikController
        [NotLoggedIn]
        public ActionResult Index()
        {
            return View("Login"); //ne znam treba li ovo
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                Korisnik korisnik = uow.Korisnik.VratiKorisnika(new Korisnik { Username = model.Username, Password = model.Password });
                HttpContext.Session.SetInt32("korisnikid", korisnik.KorisnikId);
                HttpContext.Session.SetString("username", korisnik.Username); //ovde definisemo da li je admin ili korisnik!!!
                HttpContext.Session.Set("korisnik", JsonSerializer.SerializeToUtf8Bytes(korisnik)); //serijalizujemo celog korisnika
                return RedirectToAction("Kurs", "Kurs");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Wrong credentials!");
                return View();
            }
        }

        [NotLoggedIn]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [NotLoggedIn]
        public ActionResult Register(RegisterViewModel model)
        {
            if (uow.Korisnik.Search(k => k.Username == model.Username).Any())
            {
                ModelState.AddModelError(string.Empty, "Username is taken!");
                return View();
            }
                uow.Korisnik.Add(new Korisnik { Ime = model.Ime, Prezime = model.Prezime, Username = model.Username, Password = model.Password, BrPoena = 0 });
                uow.Commit();
                return RedirectToAction("Kurs", "Kurs"); //idi na Login stranicu sad ako se uspesno registrovao
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // GET: KorisnikController/Details/5
        /* public ActionResult Details(int id)
         {
             return View();
         }*/

        // GET: KorisnikController/Create
        
        public ActionResult Create()
        {
            List<Kurs> list = uow.Kurs.GetAll();
            List<SelectListItem> selectList = list.Select(k => new SelectListItem { Text = k.NazivKursa, Value = k.KursId.ToString()}).ToList();
            CreateKorisnikViewModel model = new CreateKorisnikViewModel
            {
                Kursevi = selectList
            };
            return View(model);
        }

        // POST: KorisnikController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateKorisnikViewModel viewmodel)
        {
            try
            {
                uow.Korisnik.Add(viewmodel.Korisnik);
                uow.Commit();
                return RedirectToAction("Kurs", "Kurs");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Create");
            }
        }

        [HttpPost]
        public ActionResult AddPohadjanje(PohadjanjeViewModel request)
        {
            Console.WriteLine(request.RedniBroj);
            string naziv = uow.Kurs.FindById(new Kurs { KursId = request.KursId }).NazivKursa;
            PohadjanjeViewModel model = new PohadjanjeViewModel
            {
                RedniBroj = request.RedniBroj,
                KursId = request.KursId,
                NazivKursa = naziv
            };
            return PartialView("PohadjanjePartial", model);
        }


    }
}
