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
        public List<Pitanje> Pitanja { get; set; } //njegova pitanja
    }
}
