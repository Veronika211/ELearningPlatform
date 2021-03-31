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
        }
        public IRepositoryKurs Kurs { get; set; }
        public IRepositoryTest Test { get; set; }
        public IRepositoryKorisnik Korisnik { get; set; }

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
