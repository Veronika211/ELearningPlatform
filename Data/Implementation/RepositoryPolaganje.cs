using Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace Data.Implementation
{
    public class RepositoryPolaganje : IRepositoryPolaganje
    {
        private Context context;

        public RepositoryPolaganje(Context context)
        {
            this.context = context;
        }
        public void Add(Polaganje s)
        {
            context.Polaganje.Add(s);
        }

        public void Delete(Polaganje s)
        {
            throw new NotImplementedException();
        }

        public Polaganje FindById(Polaganje polaganje)
        {
            return context.Polaganje.Single(p => p.PolaganjeId == polaganje.PolaganjeId);
        }

        public List<Polaganje> GetAll()
        {
            return context.Polaganje.ToList();
        }

        public List<Polaganje> Search(Expression<Func<Polaganje, bool>> pred)
        {
            throw new NotImplementedException();
        }

        public void Update(Polaganje s)
        {
            throw new NotImplementedException();
        }
    }
}
