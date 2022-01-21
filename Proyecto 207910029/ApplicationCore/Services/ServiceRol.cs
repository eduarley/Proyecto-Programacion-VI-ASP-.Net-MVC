using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRol : IServiceRol
    {
        public void DeleteRol(int id)
        {
            IRepositoryRol repository = new RepositoryRol(); //importante poner public en los repositories
            repository.DeleteRol(id);
        }

        public IEnumerable<Rol> GetRol()
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetRol();
        }

        public Rol GetRolByID(int id)
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetRolByID(id);
        }

        public Rol GetRolPorUsuario(string id)
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetRolPorUsuario(id);
        }

        public Rol Save(Rol rol)
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.Save(rol);
        }
    }
}
