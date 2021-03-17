using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    
    public class Checkbox: Pitanje
    {
        public string TacanOdgovor { get; set; }
        public int TacanBodovi { get; set; }
        public string NetacanOdgovor1 { get; set; }
        public string NetacanOdgovor2 { get; set; }
        public string NetacanOdgovor3 { get; set; }
    }
}
