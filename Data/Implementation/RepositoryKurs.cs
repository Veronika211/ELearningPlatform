using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            if (k.Lekcije != null)
            {
                foreach (Lekcija l in k.Lekcije.ToList())
                {
                    k.Lekcije.Remove(l); ; //obrisi sve lekcije vezane za taj kurs, ali brise samo iz liste ostace u bazi??
                    //trebalo bi da se izbrisu i iz baze ali ne znam kako, mozda ce samo
                }
            }
           
            if (k.Testovi.Count>0)
            {
                foreach (Test t in k.Testovi)
                {
                    foreach (Pitanje p in t.Pitanja)
                    {
                        context.Pitanja.Remove(p); //obrisati prvo sva povezana pitanja
                    }
                    context.Testovi.Remove(t);//ovde izbacim iz baze
                }
            }
            //sad bi trebalo da radi xd
            context.Kursevi.Remove(k);
        }


        public Kurs FindById(Kurs kurs)
        {
            return context.Kursevi.SingleOrDefault(k => k.KursId == kurs.KursId);
        }

        public List<Kurs> GetAll()
        {
            return context.Kursevi.ToList();
        }

        public List<Kurs> Search(Expression<Func<Kurs, bool>> k)
        {
            return context.Kursevi.Where(k).ToList();
        }

        public void Update(Kurs s)
        {
            throw new NotImplementedException();
        }
    }
}
