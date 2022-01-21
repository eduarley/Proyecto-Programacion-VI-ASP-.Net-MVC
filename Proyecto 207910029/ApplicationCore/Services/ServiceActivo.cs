using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceActivo:IServiceActivo
    {
        public void DeleteActivo(int id)
        {
            IRepositoryActivo repository = new RepositoryActivo(); //importante poner public en los repositories
            repository.DeleteActivo(id);
        }

        public IEnumerable<Activo> GetActivo()
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivo();
        }

        public Activo GetActivoByID(int id)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivoByID(id);
        }

        public IEnumerable<Activo> GetActivosPorRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivosPorRangoFechas(fechaInicio, fechaFin);
        }

        
        public Activo Save(Activo activo)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            ServiceBCCR serviceBCCR = new ServiceBCCR();
            activo.CostoDolar = activo.CostoColon / (Decimal)serviceBCCR.GetDolar(DateTime.Now, DateTime.Now, "c");
            activo.ValorActual = activo.CostoColon;
            return repository.Save(activo);
        }
    }
}
