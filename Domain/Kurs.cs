using Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Kurs
    {
        public int KursId { get; set; }
        [Required(ErrorMessage ="Ovde unesite naziv kursa.")]
        [NazivKursaValidation(ErrorMessage = "Naziv kursa ne moze biti kraci od tri slova.")]
        public string NazivKursa { get; set; }
        public List<Pohadjanje> Korisnici { get; set; }
        public List<Lekcija> Lekcije { get; set; }
        public List<Test> Testovi { get; internal set; }
        //lista testova
    }
}
