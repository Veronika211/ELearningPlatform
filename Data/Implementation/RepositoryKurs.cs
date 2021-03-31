using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Implementation
{
    public class RepositoryKurs : IRepositoryKurs
    {
        private Context context;

        public RepositoryKurs(Context context)
        {
            this.context = context; //ovako da bismo mogli sve objekte da sacuvamo pod jednom trans.
        }
        public void Add(Kurs k)
        {
            context.Kursevi.Add(k);
        }

        public void Delete(Kurs k)
        {
            context.Kursevi.Remove(k);
        }

        public Kurs FindById(Kurs kurs)
        {
            return context.Kursevi.Single(k => k.KursId == kurs.KursId);
        }

        public List<Kurs> GetAll()
        {
            return context.Kursevi.ToList();
        }
    }
}
