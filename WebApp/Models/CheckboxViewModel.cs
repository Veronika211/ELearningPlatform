using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CheckboxViewModel
    {
        public string TacanOdgovor { get; set; }
        public int TacanBodovi { get; set; }
        public string NetacanOdgovor1 { get; set; }
        public string NetacanOdgovor2 { get; set; }
        public string NetacanOdgovor3 { get; set; }
        public string Odgovor{ get; set; }
        public string Naziv { get; set; }
        public int PitanjeId { get; set; }
    }
}
