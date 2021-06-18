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
            foreach(Pitanje p in t.Pitanja)
            {
                context.Pitanja.Remove(p); //obrisati prvo sva povezana pitanja
            }
            context.Testovi.Remove(t); //obrisati test iz tabele
        }

        public Test FindById(Test test)
        {
            return context.Testovi.SingleOrDefault(t => t.TestId == test.TestId);
        } //nalazim pomocu test id-a i kurs id-a

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
