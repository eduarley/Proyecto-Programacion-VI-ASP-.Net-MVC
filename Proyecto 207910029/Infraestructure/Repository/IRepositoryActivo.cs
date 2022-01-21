using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryActivo
    {
        IEnumerable<Activo> GetActivo();
        Activo GetActivoByID(int id);
        void DeleteActivo(int id);
        Activo Save(Activo activo);

        IEnumerable<Activo> GetActivosPorRangoFechas(DateTime fechaInicio, DateTime fechaFin);


        

    }
}
