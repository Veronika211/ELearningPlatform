using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Lekcija
    {
        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
        public int LekcijaId { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }
    }
}
