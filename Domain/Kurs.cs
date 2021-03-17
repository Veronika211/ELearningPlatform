using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Kurs
    {
        public int KursId { get; set; }
        public string NazivKursa { get; set; }
        public List<Pohadjanje> Korisnici { get; set; }
        public List<Lekcija> Lekcije { get; set; }
    }
}
