using System;
using System.Collections.Generic;

namespace Domain
{
    public class Korisnik
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int BrPoena { get; set; }
        public List<Pohadjanje> Kursevi { get; set; }
        public List<Polaganje> Testovi { get; set; }

        public override string ToString()
        {
            return $"{KorisnikId}: {Ime} {Prezime}";
        }
    }
}
