using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Implementation
{
    public class RepositoryTest : IRepositoryTest
    {
        private Context context;

        public RepositoryTest(Context context)
        {
            this.context = context; 
        }
        public void Add(Test t)
        {
            context.Testovi.Add(t);
        }

        public void Delete(Test t)
        {
            context.Testovi.Remove(t);
        }

        public Test FindById(Test test)
        {
            return context.Testovi.Include(t => t.Kurs).Single(t => t.TestId == test.TestId && t.KursId == test.KursId);
        }

        public List<Test> GetAll()
        {
            return context.Testovi.ToList();
        }

        public List<Test> Search(Expression<Func<Test, bool>> pred)
        {
            throw new NotImplementedException();
        }

        public void Update(Test s)
        {
            throw new NotImplementedException();
        }
    }
}
