using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    //definisemo sta je to sto nam treba na stranici
    public class ViewModel
    {
        public List<SelectListItem> Kursevi { get; set; }
        public string Nivo { get; set; }
        public int KursId { get; set; }
        public int TestId { get; set; }

    }
}
