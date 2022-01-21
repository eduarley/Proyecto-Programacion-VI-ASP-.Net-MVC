using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryDepreciacion
    {
        IEnumerable<Depreciacion> GetDepreciacionPorIdActivo(int id);

        Depreciacion Save(Activo activo, Depreciacion depreciacion);

        void DeleteDepreciacion(int id);
        IEnumerable<Depreciacion> GetDepreciacionPorFecha(DateTime fecha);

        Depreciacion ObtenerUltimaDepreciacionPorActivo(int id);
    }
}
