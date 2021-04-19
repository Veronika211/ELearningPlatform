using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Implementation
{
    public class RepositoryKorisnik : IRepositoryKorisnik
    {
        private Context context;

        public RepositoryKorisnik(Context context)
        {
            this.context = context;
        }
        public void Add(Korisnik k)
        {
            context.Korisnici.Add(k);
        }

        public void Delete(Korisnik k)
        {
            context.Korisnici.Remove(k);
        }

        public Korisnik FindById(Korisnik korisnik)
        {
            return context.Korisnici.Single(k => k.KorisnikId == korisnik.KorisnikId);
        }

        public List<Korisnik> GetAll()
        {
            return context.Korisnici.ToList();
        }

        public List<Pohadjanje> PohadjaniKursevi(Korisnik k)
        {
            throw new NotImplementedException();
        }

        public List<Polaganje> PolaganiTestovi(Korisnik k)
        {
            throw new NotImplementedException();
        }

        public List<Korisnik> Search(Expression<Func<Korisnik, bool>> pred)
        {
            return context.Korisnici.Where(pred).ToList();
        }

        public Korisnik VratiKorisnika(Korisnik korisnik)
        {
            return context.Korisnici.Single(k => k.Username == korisnik.Username && k.Password == korisnik.Password);
        }
    }
}
