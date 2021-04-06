using Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Data
{
    public interface IRepositoryKurs : IRepository<Kurs>
    {
        //ovde valjda mogu da dodam ako ima neka posebna funkcija samo za Kurs
        List<Kurs> Search(Expression<Func<Kurs, bool>> k);
    }

}
