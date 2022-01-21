using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryRol
    {
        IEnumerable<Rol> GetRol();
        Rol GetRolByID(int id);
        void DeleteRol(int id);
        Rol Save(Rol rol);
        Rol GetRolPorUsuario(string id);
    }
}
