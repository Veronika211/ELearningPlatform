using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Polaganje
    {
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int BodoviT { get; set; }
    }
}
