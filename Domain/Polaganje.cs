using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Polaganje
    {
        public int PolaganjeId { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public Test Test { get; set; }
        public int BodoviT { get; set; }
    }
}
