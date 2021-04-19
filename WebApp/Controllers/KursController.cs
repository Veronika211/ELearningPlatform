using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Data.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [LoggedInKorisnik]
    public class KursController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public KursController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //samo ovoj metodi moze da pristupi neko ko nije trenutno prijavljen
        
        public ActionResult Kurs()
        {
            List<Kurs> model = unitOfWork.Kurs.GetAll();
            int? korisnikid = HttpContext.Session.GetInt32("korisnikid"); //vraca null ako ne postoji, zato ?
            Console.WriteLine(korisnikid);
            if (korisnikid != null)
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.Username = HttpContext.Session.GetString("username");
                byte[] korisnikBy = HttpContext.Session.Get("korisnik");
                Korisnik korisnik = JsonSerializer.Deserialize<Korisnik>(korisnikBy); //ovako isto mozemo proveriti da li je admin ili korisnik
            }
            else return RedirectToAction("Index", "Korisnik");
            return View("Kurs", model);
        }
        [NotLoggedIn]
        public ActionResult Details([FromRoute(Name="id")] int id)
        {
            Kurs model = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateKurs");
        }
        //ovako postavljamo da radi samo za http post metode, nece imati problem da prepozna sta da pozove(koji endpoint)
        [HttpPost]
        [ValidateAntiForgeryToken] //da se metoda ne izvrsi ukoliko neko hoce da nas napadne, da se podaci sigurno unose sa nase forme
        public ActionResult Create([FromForm]Kurs kurs)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateKurs");
            }
            bool exists = unitOfWork.Kurs.Search(k => k.NazivKursa == kurs.NazivKursa).Any();
            if (exists)
            {
                ModelState.AddModelError("NazivKursaValidation", "Ovaj kurs vec postoji!");
                    return View("CreateKurs");
            }
            unitOfWork.Kurs.Add(kurs);
            unitOfWork.Commit();
            return Kurs();
        }
    }
}
