using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Administrator
    {
        public int AdministratorId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
