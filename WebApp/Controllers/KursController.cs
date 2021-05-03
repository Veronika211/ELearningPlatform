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
    
    public class KursController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public KursController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
       [LoggedInAdministrator] //treba i korisnik da vidi
        public ActionResult Kurs()
        {
            List<Kurs> model = unitOfWork.Kurs.GetAll();
            int? korisnikid = HttpContext.Session.GetInt32("korisnikid"); //vraca null ako ne postoji, zato ?
            int? administratorid = HttpContext.Session.GetInt32("administratorid");
            if (korisnikid != null)
            {
                ViewBag.IsLoggedInKorisnik = true;
                ViewBag.Username = HttpContext.Session.GetString("username");
                byte[] korisnikBy = HttpContext.Session.Get("korisnik");
                Korisnik korisnik = JsonSerializer.Deserialize<Korisnik>(korisnikBy); //ovako isto mozemo proveriti da li je admin ili korisnik
            }
            else if (administratorid != null)
            {
                ViewBag.IsLoggedInAdministrator = true;
                ViewBag.Username = HttpContext.Session.GetString("username");
                byte[] adminBy = HttpContext.Session.Get("administrator");
                Administrator administrator = JsonSerializer.Deserialize<Administrator>(adminBy); //ovako isto mozemo proveriti da li je admin ili korisnik
            }
            else return RedirectToAction("Index", "Korisnik");
            return View("Kurs", model);
        }

        [LoggedInAdministrator]
        public ActionResult Delete(int id)
        {
            Kurs model = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            unitOfWork.Kurs.Delete(model);
            unitOfWork.Commit();
            return RedirectToAction("Kurs","Kurs");
        }

        [LoggedInAdministrator] //treba da i korisnik moze da procita
        public ActionResult PrikaziSadrzaj([FromRoute(Name ="id")] int lekcijaId)
        {
            Lekcija l = new Lekcija { LekcijaId = lekcijaId }; //nadji kurs koji ima lekciju sa ovim Id-em
            List<Kurs> kursevi = unitOfWork.Kurs.GetAll();
            Lekcija nova = new Lekcija();
            foreach(Kurs k in kursevi)
            {
                nova = k.Lekcije.Single(lek => lek.LekcijaId == l.LekcijaId); //nadjem lekciju
                if (nova != null)
                {
                    break;
                }
            }
            HttpContext.Session.SetString("id", lekcijaId.ToString());
            HttpContext.Session.SetString("sadrzaj", nova.Sadrzaj);
            HttpContext.Session.SetString("naziv", nova.Naziv);
            ViewBag.Sadrzaj = HttpContext.Session.GetString("sadrzaj");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Naziv = HttpContext.Session.GetString("naziv");
            return View("SadrzajLekcije");
        }
        [LoggedInAdministrator] //proveri mislim da ovo nije implementirano nikako
        public ActionResult Edit(int id)
        {
            Kurs model = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            unitOfWork.Kurs.Update(model);
            unitOfWork.Commit();
            return RedirectToAction("Kurs", "Kurs");
        }

        [LoggedInAdministrator]
        public ActionResult Details([FromRoute(Name="id")] int id)
        {
            Kurs model = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            return View(model);
        }
        
        [HttpGet]
        [LoggedInAdministrator]
        public ActionResult Create()
        {
            return View("CreateKurs");
        }
        //ovako postavljamo da radi samo za http post metode, nece imati problem da prepozna sta da pozove(koji endpoint)
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        [LoggedInAdministrator]
        //da se metoda ne izvrsi ukoliko neko hoce da nas napadne, da se podaci sigurno unose sa nase forme
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
