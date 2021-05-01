using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IRepositoryAdministrator: IRepository<Administrator>
    {
        Administrator VratiAdministratora(Administrator a); //preko imena i sifre
    }
}
