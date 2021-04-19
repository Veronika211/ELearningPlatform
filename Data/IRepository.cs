using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data
{
    public interface IRepository<T>
    {
        void Add(T s);
        List<T> GetAll();
        T FindById(T id);
        void Delete(T s);
        List<T> Search(Expression<Func<T,bool>> pred);
    }
}
