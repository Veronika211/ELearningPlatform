using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    
    public class Dopuna: Pitanje
    {
        public string TacanOdgovor { get; set; }
        public int TacanBodovi { get; set; }
    }
}
