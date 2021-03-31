using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IRepositoryKorisnik: IRepository<Korisnik>
    {
        List<Polaganje> PolaganiTestovi(Korisnik k);
        List<Pohadjanje> PohadjaniKursevi(Korisnik k);
        Korisnik VratiKorisnika(Korisnik k); //preko imena i sifre
    }
}
