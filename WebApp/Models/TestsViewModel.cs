using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TestsViewModel
    {
        public List<Test> Testovi { get; set; }
        public int TestId { get; set; }
        public List<Dopuna> Dopune{ get; set; } //njegova pitanja koja treba da se prikazu
        public List<Checkbox> Checkboxes { get; set; } //isto pitanja zatvorena
        //sad bi trebalo da se TestId prosledi i da se dodele vrednosti za Dopune i Checkboxes
    }
}
