using Data.Implementation;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public class UnitOfWorkImplementation : IUnitOfWork
    {
        private Context context;
        public UnitOfWorkImplementation(Context context)
        {
            this.context = context;
            Kurs = new RepositoryKurs(context);
            Test = new RepositoryTest(context);
            Korisnik = new RepositoryKorisnik(context);
            Administrator = new RepositoryAdministrator(context);
            Pitanje = new RepositoryPitanje(context);
            Pohadjanje = new RepositoryPohadjanje(context);
            Polaganje = new RepositoryPolaganje(context);
        }
        public IRepositoryKurs Kurs { get; set; }
        public IRepositoryTest Test { get; set; }
        public IRepositoryKorisnik Korisnik { get; set; }
        public IRepositoryPitanje Pitanje { get; set; }
        public IRepositoryPohadjanje Pohadjanje { get; set; }

        public IRepositoryAdministrator Administrator { get; set; }
        public IRepositoryPolaganje Polaganje { get; set; }
        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
