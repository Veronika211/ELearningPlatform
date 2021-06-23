using Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Data.Implementation
{
    public class RepositoryPohadjanje : IRepositoryPohadjanje
    {
        private Context context;

        public RepositoryPohadjanje(Context context)
        {
            this.context = context;
        }
        public void Add(Pohadjanje s)
        {
            context.Pohadjanje.Add(s);
        }

        public void Delete(Pohadjanje s)
        {
            throw new NotImplementedException();
        }

        public Pohadjanje FindById(Pohadjanje id)
        {
            throw new NotImplementedException();
        }

        public List<Pohadjanje> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Pohadjanje> Search(Expression<Func<Pohadjanje, bool>> pred)
        {
            throw new NotImplementedException();
        }

        public void Update(Pohadjanje s)
        {
            throw new NotImplementedException();
        }
    }
}
