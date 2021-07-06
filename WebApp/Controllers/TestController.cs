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
        [LoggedInKorisnik]
        public ActionResult PrikaziTestKorsnik([FromRoute(Name = "id")] int kursId,TestsViewModel tvm)
        {
            ViewBag.IsLoggedInKorisnik = true;
            //za ovaj kurs samo da se prikazu testovi
           // int? kursid = HttpContext.Session.GetInt32("kursid"); 
           int idKorisnika = (int)HttpContext.Session.GetInt32("korisnikid");
            //proverim da li postoji polaganje sa ovim korisnikom i svakim od testova, ako 
            //ne postoji onda prikazati test,
            //ako postoji ne prikazuj ga jer ne moze da ga polaze opet xd
            tvm.Testovi = uow.Test.GetAll().Where(t => t.KursId == kursId).ToList();
            //ovo su svi testovi tog kursa
            //mozda mogu da brisem ako postoji i da vratim ovaj tvm
            List<Test> testovi = new List<Test>();
            foreach(Test t in tvm.Testovi)
            {
                Boolean exists = false;
                foreach (Polaganje p in uow.Polaganje.GetAll())
                { //ako je polaganje.testid drugaciji onda prikazi taj test tj dodaj u tvm.testovi.add
                    
                    if (p.KorisnikId == idKorisnika && p.TestId == t.TestId)
                    {
                        //onda postoji to polaganje za tog korisnika vec i ne prikazuj test
                        exists = true;
                        break;
                    }
                }
                if (exists)
                {
                    testovi.Add(t);//posle prodje kroz njih i sve koji su zajednicki obrisem
                    //iz tvm modela
                }

            }
            foreach(Test t in testovi)
            {
                if (tvm.Testovi.Contains(t))
                {
                    tvm.Testovi.Remove(t);
                }
            }
            return View("TestKorisnik",tvm);
        }

        // GET: TestController/Details/5
        [LoggedInAdministrator]
        public ActionResult Details([FromRoute(Name = "id")] int id, TestsViewModel tvm)
        {
            ViewBag.IsLoggedInAdministrator = true;
            tvm.TestId = id;
            HttpContext.Session.SetInt32("testid",id); //ovo koristim kad dodajem novo pitanje za taj test
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
            List<Polaganje> polaganja = uow.Polaganje.GetAll();
            int korisnikid = (int)HttpContext.Session.GetInt32("korisnikid");
            Korisnik k = uow.Korisnik.FindById(new Korisnik { KorisnikId = korisnikid });
            int idTesta = id;
            Test t = uow.Test.FindById(new Test { TestId = id });
            if (polaganja.Count>0)
            {
                //ako lista nije prazna trazimo ovog korisnika i ovaj test
                //ako ga vec nema dodati ga u bazu, a ako ga ima preskociti izbaciti korisnika
                //kasnije implementirati da tu iskoci poruka da je ovaj test vec radio ili sta vec
                bool exists = false;
                foreach(Polaganje p in polaganja)
                {
                    if(p.TestId==idTesta && p.KorisnikId == korisnikid)
                    {
                        exists = true;
                        break;
                    }
                }//vraca da li polaganje vec postoji, ako ne postoji dodati ga
                if (!exists)
                {
                    uow.Polaganje.Add(new Polaganje
                    {
                        Korisnik = k,
                        KorisnikId = korisnikid,
                        Test = t,
                        TestId = idTesta
                    });
                    uow.Commit();
                }
            }
            else
            {
                uow.Polaganje.Add(new Polaganje
                {
                    Korisnik = k,
                    KorisnikId = korisnikid,
                    Test = t,
                    TestId = idTesta
                });
                uow.Commit();
            }
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

        [LoggedInAdministrator]
        public ActionResult PogledajPitanjeCheckbox([FromRoute(Name = "id")] int id)
        {
            ViewBag.IsLoggedInAdministrator = true;
            Checkbox model = (Checkbox)uow.Pitanje.FindById(new Pitanje { PitanjeId = id }); //ne znam da li moze ovako
            return View("PitanjeCheckbox", model);
        }

        [LoggedInAdministrator]
        public ActionResult UnosPitanjaNaDopunu()
        {
            ViewBag.IsLoggedInAdministrator = true;
            return View("UnosPitanjaNaDopunu");
        }

        [LoggedInAdministrator]
        [HttpPost]
        public ActionResult DodajPitanjeNaDopunu(Dopuna model)
        {
            ViewBag.IsLoggedInAdministrator = true;
            int testId = (int)HttpContext.Session.GetInt32("testid");
            model.TestId = testId;
            uow.Pitanje.Add(new Dopuna
            {
                TestId = model.TestId,
                TacanOdgovor = model.TacanOdgovor,
                Naziv = model.Naziv,
                TacanBodovi = model.TacanBodovi
            });
            uow.Commit();
            return View("PitanjeDopuna",model);
        }

        [LoggedInAdministrator]
        public ActionResult UnosPitanjaCheckbox()
        {
            ViewBag.IsLoggedInAdministrator = true;
            return View("UnosPitanjaCheckbox");
        }

        [LoggedInAdministrator]
        [HttpPost]
        public ActionResult DodajPitanjeCheckbox(Checkbox model)
        {
            ViewBag.IsLoggedInAdministrator = true;
            //prosledjeni id bi trebalo da je id testa
            int testId = (int)HttpContext.Session.GetInt32("testid");
            model.TestId = testId;
            uow.Pitanje.Add(new Checkbox
            {
                Naziv = model.Naziv,
                TacanOdgovor = model.TacanOdgovor,
                TacanBodovi = model.TacanBodovi,
                NetacanOdgovor1 = model.NetacanOdgovor1,
                NetacanOdgovor2 = model.NetacanOdgovor2,
                NetacanOdgovor3 = model.NetacanOdgovor3,
                TestId = model.TestId
            }) ;
            uow.Commit();
            return View("PitanjeCheckbox", model);
        }

        [LoggedInKorisnik]

        public ActionResult PitanjaDopunaKorisnik([FromRoute(Name = "id")] int id)
        {
            ViewBag.IsLoggedInKorisnik = true;
            Dopuna model = (Dopuna)uow.Pitanje.FindById(new Dopuna { PitanjeId = id });
            return View("PitanjaDopunaKorisnik", model);
        }

        [LoggedInKorisnik]
        [HttpPost]
        //ova metoda sluzi da proveri da li je korisnik uneo tacan odgovor
        //potrebno je preneti 
        public ActionResult PotvrdiOdgovorDopuna(Dopuna model)
        {
            ViewBag.IsLoggedInKorisnik = true;
            string korisnikovOdgovor = model.TacanOdgovor;
            Dopuna pitanje = (Dopuna)uow.Pitanje.FindById(new Dopuna { PitanjeId=model.PitanjeId});
            int idKorisnika =(int)HttpContext.Session.GetInt32("korisnikid");
            Korisnik k = uow.Korisnik.FindById(new Korisnik { KorisnikId = idKorisnika });
            int idTesta = pitanje.TestId;
            foreach (Polaganje p in uow.Polaganje.GetAll())
            {
                if (p.KorisnikId == idKorisnika && p.TestId == idTesta)
                {

                    if (pitanje.TacanOdgovor == korisnikovOdgovor)
                    {
                        p.BodoviT += pitanje.TacanBodovi;
                        k.BrPoena += pitanje.TacanBodovi;
                        uow.Commit();
                        break;
                    }
                }
            }

            TestsViewModel tvm = new TestsViewModel();
            tvm.TestId = idTesta;
            List<Pitanje> pitanja = uow.Pitanje.GetAll().Where(p => p.TestId == tvm.TestId).ToList();
            List<Checkbox> checkboxes = pitanja.OfType<Checkbox>().ToList();
            List<Dopuna> dopune = pitanja.OfType<Dopuna>().ToList();
            //namestiti da linkovi za pitanja koja su vec pogadjana budu disable
            //tj da ne moze u isto pitanje da se udje dva puta
            //isto vazi i za test
            tvm.Checkboxes = checkboxes;
            tvm.Dopune = dopune;
            return View("PitanjaKorisnik", tvm);
        }

        [LoggedInKorisnik]

        public ActionResult PitanjaCheckboxKorisnik([FromRoute(Name = "id")] int id)
        {
            ViewBag.IsLoggedInKorisnik = true;
            Checkbox model = (Checkbox)uow.Pitanje.FindById(new Dopuna { PitanjeId = id });
            CheckboxViewModel cvm = new CheckboxViewModel
            {
                Naziv = model.Naziv,
                NetacanOdgovor1 = model.NetacanOdgovor1,
                NetacanOdgovor2 = model.NetacanOdgovor2,
                NetacanOdgovor3 = model.NetacanOdgovor3,
                PitanjeId = model.PitanjeId,
                TacanOdgovor = model.TacanOdgovor,
                TacanBodovi = model.TacanBodovi
            };
            HttpContext.Session.SetInt32("idCheckbox", model.PitanjeId);
            return View("PitanjaCheckboxKorisnik", cvm);
        }


        [LoggedInKorisnik]
        [HttpPost]
        public ActionResult PotvrdiOdgovorCheckbox(CheckboxViewModel model)
        {
            ViewBag.IsLoggedInKorisnik = true;
            int id = (int)HttpContext.Session.GetInt32("idCheckbox");
            Checkbox pitanje = (Checkbox)uow.Pitanje.FindById(new Checkbox { PitanjeId = id });
            //nadjem ovo pitanje i onda imam i njegov tacan odgovor
            int idKorisnika = (int)HttpContext.Session.GetInt32("korisnikid");
            Korisnik k = uow.Korisnik.FindById(new Korisnik { KorisnikId = idKorisnika });
            int idTesta = pitanje.TestId; //nadjemo korisnika i nadjemo test
            foreach (Polaganje p in uow.Polaganje.GetAll())
            {
                if (p.KorisnikId == idKorisnika && p.TestId == idTesta)
                {
                    if (model.Odgovor == pitanje.TacanOdgovor)
                    {
                        p.BodoviT += pitanje.TacanBodovi;
                        k.BrPoena += pitanje.TacanBodovi;
                        uow.Commit();
                        break;
                    } 
                }
            }
            TestsViewModel tvm = new TestsViewModel();
            tvm.TestId = idTesta;
            List<Pitanje> pitanja = uow.Pitanje.GetAll().Where(p => p.TestId == tvm.TestId).ToList();
            List<Checkbox> checkboxes = pitanja.OfType<Checkbox>().ToList();
            List<Dopuna> dopune = pitanja.OfType<Dopuna>().ToList();
            //namestiti da linkovi za pitanja koja su vec pogadjana budu disable
            //tj da ne moze u isto pitanje da se udje dva puta
            //isto vazi i za test
            tvm.Checkboxes = checkboxes;
            tvm.Dopune = dopune;
            return View("PitanjaKorisnik", tvm);
        }


    }
}
