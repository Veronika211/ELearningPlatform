using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Pitanje
    {
        public int PitanjeId { get; set; }
        public string Naziv { get; set; }
    }
}
