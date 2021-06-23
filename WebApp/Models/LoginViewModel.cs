using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string KorisnikORAdministrator { get; set; }
        
        //onaj koji je null od ove dvojice nije ulogovan
    }
}
