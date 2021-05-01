using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Implementation
{
    public class RepositoryAdministrator : IRepositoryAdministrator
    {
        private Context context;
        public RepositoryAdministrator(Context context)
        {
            this.context = context;
        }

        public void Add(Administrator s)
        {
            throw new NotImplementedException();
        }

        public void Delete(Administrator s)
        {
            throw new NotImplementedException();
        }

        public Administrator FindById(Administrator id)
        {
            throw new NotImplementedException();
        }

        public List<Administrator> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Administrator> Search(Expression<Func<Administrator, bool>> pred)
        {
            throw new NotImplementedException();
        }

        public void Update(Administrator s)
        {
            throw new NotImplementedException();
        }

        public Administrator VratiAdministratora(Administrator a)
        {
            return context.Administratori.Single(adm => adm.Username == a.Username && adm.Password == a.Password);
        }
    }
}
