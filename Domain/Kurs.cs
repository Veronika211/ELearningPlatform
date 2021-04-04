using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Kurs
    {
        public int KursId { get; set; }
        [Required]
        public string NazivKursa { get; set; }
        public List<Pohadjanje> Korisnici { get; set; }
        public List<Lekcija> Lekcije { get; set; }
    }
}
