using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Implementation
{
    public class RepositoryPitanje : IRepositoryPitanje
    {
        private Context context;
        public RepositoryPitanje(Context context)
        {
            this.context = context;
        }
        public void Add(Pitanje s)
        {
            context.Pitanja.Add(s);
        }

        public void Delete(Pitanje s)
        {
            context.Pitanja.Remove(s);
        }

        public Pitanje FindById(Pitanje pitanje)
        {
            return context.Pitanja.Single(p => p.PitanjeId == pitanje.PitanjeId);
        }

        public List<Pitanje> GetAll()
        {
            return context.Pitanja.ToList();
        }

        public List<Pitanje> Search(Expression<Func<Pitanje, bool>> pred)
        {
            throw new NotImplementedException();
        }

        public void Update(Pitanje s)
        {
            throw new NotImplementedException();
        }
    }
}
