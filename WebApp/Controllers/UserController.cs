using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Data;
using Data.UnitOfWork;

namespace WebApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public UserController(IUnitOfWork uow)
        {
            this.uow = uow;
        }


        [HttpGet("courses")]
        public IActionResult GetCourses()
        {
            var kursevi = uow.Kurs.GetAll();
            if (kursevi == null)
                return NotFound();
            else return Ok(kursevi);
        }



        [Route("Delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var k = uow.Kurs.FindById(new Kurs { KursId = id });
            if (k == null)
                return NotFound();
            else
            {
                uow.Kurs.Delete(k);
                uow.Commit();
            }
            return Ok();
        }

        [Route("Post")]
        [HttpPost]
        public IActionResult Post(Kurs kurs)
        {
            if (!ModelState.IsValid)
                return BadRequest("Lose uneti podaci.");

            uow.Kurs.Add(new Kurs
            {
                KursId = kurs.KursId,
                NazivKursa = kurs.NazivKursa
            });
            uow.Commit();
            return Ok();
        }

        [Route("Put/{id}")]
        [HttpPut]
        public IActionResult Put(Kurs kurs)
        {

            if (!ModelState.IsValid)
                return BadRequest("Nisu odgovarajuci podaci.");


            var k = uow.Kurs.FindById(new Kurs { KursId = kurs.KursId });
            //nadjemo ovog korisnika pa ga menjamo
            if (k != null)
            {
                k.NazivKursa = kurs.NazivKursa;
                uow.Commit();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
    }

}

