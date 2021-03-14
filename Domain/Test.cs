using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Test
    {
        public int KursId { get; set; }
        public Kurs Kurs { get; set; } //nisam sigurna, valjda deo primarnog kljuca i spoljni ujedno
        public int TestId { get; set; }
        public string Nivo { get; set; }
    }
}
