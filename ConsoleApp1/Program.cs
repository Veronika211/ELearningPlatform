using Data;
using Data.Implementation;
using Data.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //using moze da se koristi nad interfejsima koji implementiraju IDisposable
            using IUnitOfWork uow = new UnitOfWorkImplementation(new Context());
           
            uow.Commit();
            //kad napisemo using ne mora eksplicitno da se poziva Dispose!!!
        }
                
    }
}
