using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceDepreciacion
    {
        IEnumerable<Depreciacion> GetDepreciacionPorIdActivo(int id);

        Depreciacion Save(Activo activo);

        void DeleteDepreciacion(int id);
        IEnumerable<Depreciacion> GetDepreciacionPorFecha(DateTime fecha);
    }
}
