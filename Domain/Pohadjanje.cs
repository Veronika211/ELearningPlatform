using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Pohadjanje
    {
      
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
        public int Bodovi { get; set; }
    }
}
