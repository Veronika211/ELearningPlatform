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
        [NotLoggedIn]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (model.KorisnikORAdministrator == "Korisnik")
                {
                    Korisnik korisnik = uow.Korisnik.VratiKorisnika(new Korisnik { Username = model.Username, Password = model.Password });
                    if (korisnik != null)
                    {
                        HttpContext.Session.SetInt32("korisnikid", korisnik.KorisnikId);
                        HttpContext.Session.SetString("username", korisnik.Username);
                        HttpContext.Session.SetInt32("id", korisnik.KorisnikId);
                        HttpContext.Session.SetInt32("idk", korisnik.KorisnikId);
                        HttpContext.Session.Set("korisnik", JsonSerializer.SerializeToUtf8Bytes(korisnik)); //serijalizujemo celog korisnika
                        
                        //ne znam da li sme da se ima ova promenljiva i kako ce se koristiti
                        return RedirectToAction("Kurs", "Kurs");
                    }
                }
                else if (model.KorisnikORAdministrator == "Administrator")
                {
                    Administrator administrator = uow.Administrator.VratiAdministratora(new Administrator { Username = model.Username, Password = model.Password });
                    if (administrator != null)
                    {
                        HttpContext.Session.SetInt32("administratorid", administrator.AdministratorId);
                        HttpContext.Session.SetString("username", administrator.Username); //ovde definisemo da li je admin ili korisnik!!!
                        HttpContext.Session.Set("administrator", JsonSerializer.SerializeToUtf8Bytes(administrator)); //serijalizujemo celog korisnika
                        return RedirectToAction("Kurs", "Kurs");
                    }
                }
                else if(model.KorisnikORAdministrator==null)
                    ModelState.AddModelError("KorisnikORAdministrator", "Cekirajte opciju Korisnik/Administrator!!!");
                return View(model);
                
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Wrong credentials!" + ex.Message);
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
                ModelState.AddModelError(string.Empty, "Korisnicko ime je vec zauzeto!");
                return View();
            }
                uow.Korisnik.Add(new Korisnik { Ime = model.Ime, Prezime = model.Prezime, Username = model.Username, Password = model.Password, BrPoena = 0 });
                uow.Commit();
                return RedirectToAction("Kurs", "Kurs"); //idi na Login stranicu sad ako se uspesno registrovao
        }
        [LoggedInAdministrator] //treba i za korisnika
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [LoggedInKorisnik]
        public ActionResult PrikazPrijavaKursa()
        {
            ViewBag.IsLoggedInKorisnik = true;
            CreateKorisnikViewModel kvm = new CreateKorisnikViewModel();
            int? id = HttpContext.Session.GetInt32("idk");
            kvm.KorisnikId = id;
            kvm.Korisnik = uow.Korisnik.FindById(new Korisnik { KorisnikId=(int)id});
            kvm.ListaSvihKurseva = uow.Kurs.GetAll();
            List<SelectListItem> selectList = kvm.ListaSvihKurseva.Select(k => new SelectListItem { Text = k.NazivKursa, Value = k.KursId.ToString() }).ToList();
            kvm.Kursevi = selectList;
            return View("DodajPohadjanje", kvm); 
        }

        // GET: KorisnikController/Details/5
        /* public ActionResult Details(int id)
         {
             return View();
         }*/

        // GET: KorisnikController/Create

     

        // POST: KorisnikController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoggedInKorisnik]
        public ActionResult Create(CreateKorisnikViewModel viewmodel)
        {
            ViewBag.IsLoggedInKorisnik = true; //ova cela metoda je za dodavanje korisnika i 
            //to necu koristiti valjda
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
        [LoggedInKorisnik]
        public ActionResult PrijavaKursa(CreateKorisnikViewModel cvm)
        {
            ViewBag.IsLoggedInKorisnik = true;
            int id = ViewBag.KorisnikId;
            cvm.KorisnikId = id;
            return RedirectToAction("Kurs", "Kurs"); 
        }

        [HttpPost]
        [LoggedInKorisnik]
        
        public ActionResult AddPohadjanje(CreateKorisnikViewModel model)
        {
            ViewBag.IsLoggedInKorisnik = true;
            int idKursa = model.KursId;
            int? idKorisnika = HttpContext.Session.GetInt32("id");
            Korisnik korisnik = uow.Korisnik.FindById(new Korisnik { KorisnikId = (int)idKorisnika }); //nadjem celog korisnika
            Kurs kurs = uow.Kurs.FindById(new Kurs { KursId = idKursa });
            Pohadjanje p = new Pohadjanje
            {
                KorisnikId = (int)idKorisnika,
                KursId = idKursa,
                Bodovi = 0
            };
            uow.Pohadjanje.Add(p);
            uow.Commit();
            return RedirectToAction("Kurs","Kurs"); //ne mora da znaci da ce ovaj da vraca
        }


    }
}
