using System;
using System.Collections.Generic;

namespace Data
{
    public interface IRepository<T>
    {
        void Add(T s);
        List<T> GetAll();
        T FindById(T id);
        void Delete(T s);
    }
}
