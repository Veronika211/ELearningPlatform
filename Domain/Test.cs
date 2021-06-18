using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Validation;
using System.Text;

namespace Domain
{
    public class Test
    {
        public int TestId { get; set; }
        public int KursId { get; set; }
        public Kurs Kurs { get; set; }        
        public string Nivo { get; set; }
        public List<Polaganje> Korisnici { get; set; }
        public List<Pitanje> Pitanja { get; set; }
    }
}
