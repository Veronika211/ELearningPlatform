using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ActionResult Index()
        {
            return View();
        }

        // GET: KorisnikController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
