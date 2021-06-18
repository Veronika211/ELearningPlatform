using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Validation;


namespace WebApp.Models
{
    
    public class ViewModel
    {
        public List<SelectListItem> Kursevi { get; set; }
  
        public string Nivo { get; set; }
        public int KursId { get; set; }
        public int TestId { get; set; }

    }
}
