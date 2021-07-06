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
       [LoggedInBoth] //treba i korisnik da vidi
        public ActionResult Kurs()
        {
            int? korisnikid = HttpContext.Session.GetInt32("korisnikid"); //vraca null ako ne postoji, zato ?
            int? administratorid = HttpContext.Session.GetInt32("administratorid");
            List<Pohadjanje> listaKurseva = unitOfWork.Pohadjanje.GetAll().Where(p => p.KorisnikId == korisnikid).ToList();//vracaju se sva pohadjanja za ovog korisnika 
            //koji je prijavljen
            List<Kurs> model = new List<Kurs>();
            foreach (Pohadjanje p in listaKurseva)
            {
                Kurs kurs = unitOfWork.Kurs.GetAll().Single(k => k.KursId == p.KursId);//vraca taj jedan kurs
                model.Add(kurs);
            }
            if (korisnikid != null)
            {
                ViewBag.IsLoggedInKorisnik = true;
                ViewBag.Username = HttpContext.Session.GetString("username");
                ViewBag.KorisnikId = HttpContext.Session.GetInt32("username");
                byte[] korisnikBy = HttpContext.Session.Get("korisnik");
                Korisnik korisnik = JsonSerializer.Deserialize<Korisnik>(korisnikBy); //ovako isto mozemo proveriti da li je admin ili korisnik
                return View("KursKorisnik", model);
            }
            else if (administratorid != null)
            {
                ViewBag.IsLoggedInAdministrator = true;
                ViewBag.Username = HttpContext.Session.GetString("username");
                byte[] adminBy = HttpContext.Session.Get("administrator");
                Administrator administrator = JsonSerializer.Deserialize<Administrator>(adminBy); //ovako isto mozemo proveriti da li je admin ili korisnik
                List<Kurs> modelAdm = unitOfWork.Kurs.GetAll().ToList();
                return View("Kurs", modelAdm);
            }
            else
            {
                return RedirectToAction("Index", "Korisnik");
            }
        }



        [LoggedInAdministrator]
        public ActionResult Delete(int id)
        {
            ViewBag.IsLoggedInAdministrator = true;
            Kurs model = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            model.Testovi = unitOfWork.Test.GetAll().Where(t => t.KursId == model.KursId).ToList();
            foreach(Test t in model.Testovi)
            {
                t.Pitanja = unitOfWork.Pitanje.GetAll().Where(p => p.TestId == t.TestId).ToList();
            }
            unitOfWork.Kurs.Delete(model);
            unitOfWork.Commit();
            return RedirectToAction("Kurs","Kurs");
        }
        [LoggedInAdministrator]
        public ActionResult IzbrisiLekciju([FromRoute(Name ="id")]int idLekcije, int kursId)
        {
            ViewBag.IsLoggedInAdministrator = true;
            Kurs kurs = unitOfWork.Kurs.FindById(new Kurs { KursId = kursId }); 
            Lekcija model = kurs.Lekcije.FirstOrDefault(l => l.LekcijaId == idLekcije);
            kurs.Lekcije.Remove(model);
            unitOfWork.Commit();
            return View("Details",kurs);
        }

        [LoggedInBoth] //treba da i korisnik moze da procita
        public ActionResult PrikaziSadrzaj([FromRoute(Name ="id")] int lekcijaId)
        {
            int? korisnikid = HttpContext.Session.GetInt32("korisnikid");
            int? administratorid = HttpContext.Session.GetInt32("administratorid");
            if (korisnikid != null)
                ViewBag.IsLoggedInKorisnik = true;
            else
                ViewBag.IsLoggedInAdministrator = true;
            Lekcija l = new Lekcija { LekcijaId = lekcijaId }; //nadji kurs koji ima lekciju sa ovim Id-em
            List<Kurs> kursevi = unitOfWork.Kurs.GetAll();
            Lekcija nova = new Lekcija();
            List<Lekcija> listaLekcija = new List<Lekcija>();
            foreach(Kurs k in kursevi)
            {
                foreach (Lekcija lek in k.Lekcije)
                {
                    listaLekcija.Add(lek);
                }
            } //napravim listu svih lekcija i sad samo uzmem ovu ciji sam id prosledila
            nova = listaLekcija.FirstOrDefault(lek => lek.LekcijaId == lekcijaId);
            HttpContext.Session.SetString("id", lekcijaId.ToString());
            HttpContext.Session.SetString("sadrzaj", nova.Sadrzaj);
            HttpContext.Session.SetString("naziv", nova.Naziv);
            ViewBag.Sadrzaj = HttpContext.Session.GetString("sadrzaj");
            ViewBag.Id = HttpContext.Session.GetString("id");
            ViewBag.Naziv = HttpContext.Session.GetString("naziv");
            return View("SadrzajLekcije");
        }
        [LoggedInAdministrator] 
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
            ViewBag.IsLoggedInAdministrator = true;
            Kurs model = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            HttpContext.Session.SetInt32("kursid", model.KursId);
            return View(model); 
        }

        [LoggedInKorisnik]
        public ActionResult DetailsKorisnik([FromRoute(Name = "id")] int id)
        {
            ViewBag.IsLoggedInKorisnik = true;
            Kurs model = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            return View(model); //ovo je kurs i za njega prikazi ono sto se trazi u View-u
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
            ViewBag.IsLoggedInAdministrator = true;
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

        
        [LoggedInAdministrator]
        public ActionResult UnosLekcije()
        {
            ViewBag.IsLoggedInAdministrator = true;
            int id = (int)HttpContext.Session.GetInt32("kursid");
            Kurs kurs = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            Lekcija model = new Lekcija { Kurs = kurs, KursId = id};
            return View("UnosLekcije", model);
        }

        [ValidateAntiForgeryToken]
        [LoggedInAdministrator]
        [HttpPost]
        public ActionResult UnosLekcije2(Lekcija model)
        {
            //valjda se prenosi lekcija i sad samo treba da je dodam u listu kod lekcija tog kursa
            ViewBag.IsLoggedInAdministrator = true;
            int id = (int)HttpContext.Session.GetInt32("kursid");
            Kurs k = unitOfWork.Kurs.FindById(new Kurs { KursId = id });
            model.Kurs = k;
            model.KursId = id;
            k.Lekcije.Add(model);
            unitOfWork.Commit();
            return View("Details", model.Kurs);
        }
    }
}
