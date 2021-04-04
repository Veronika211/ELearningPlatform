using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class KursController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public KursController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ActionResult Kurs()
        {
            List<Kurs> model = unitOfWork.Kurs.GetAll();
            return View("Kurs", model);
        }

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
            unitOfWork.Kurs.Add(kurs);
            unitOfWork.Commit();
            return Kurs();
        }
    }
}
