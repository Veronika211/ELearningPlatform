using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CreateKorisnikViewModel
    {
        public Korisnik Korisnik { get; set; }
        public int? KorisnikId { get; set; }
        public List<SelectListItem> Kursevi { get; set; }

        public List<Kurs> ListaSvihKurseva { get; set; }
        public int KursId { get; set; } //ovaj kurs za ovog korisnika se samo dodaje
    }
}
