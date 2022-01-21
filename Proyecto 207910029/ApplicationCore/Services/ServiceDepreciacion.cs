using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDepreciacion : IServiceDepreciacion
    {
        public void DeleteDepreciacion(int id)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            repository.DeleteDepreciacion(id);
        }

        public IEnumerable<Depreciacion> GetDepreciacionPorFecha(DateTime fecha)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            return repository.GetDepreciacionPorFecha(fecha);
        }

        public IEnumerable<Depreciacion> GetDepreciacionPorIdActivo(int id)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            return repository.GetDepreciacionPorIdActivo(id);
        }

        public Depreciacion Save(Activo activo)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            



            Depreciacion depreciacion = ObtenerUltimaDepreciacionPorActivo(activo.Id);
            if (depreciacion != null)
                if (depreciacion.Fecha.Month == DateTime.Now.Month)
                    return null;


            try
            {
                if (depreciacion != null)
                {
                    var valorActivo = activo.CostoColon;
                    var vidaUtil = activo.TipoActivo.VidaUtil;
                    var depreciacionAnual = valorActivo / vidaUtil;
                    var depreciacionMensual = depreciacionAnual / 12;
                    var depreciacionAcumulada = depreciacion.DepreciacionAcumulada + depreciacionMensual;


                    depreciacion.DepreciacionMes = depreciacionMensual;
                    depreciacion.valorResidual = activo.ValorActual - depreciacion.DepreciacionMes;
                    depreciacion.DepreciacionAcumulada = depreciacionAcumulada;
                    depreciacion.Fecha = DateTime.Now;

                    activo.ValorActual = depreciacion.valorResidual;


                    

                    return repository.Save(activo,depreciacion);

                }
                else
                {
                    var valorActivo = activo.ValorActual;
                    var vidaUtil = activo.TipoActivo.VidaUtil;
                    var depreciacionAnual = valorActivo / vidaUtil;
                    var depreciacionMensual = depreciacionAnual / 12;
                    depreciacion = new Depreciacion()
                    {
                        IdActivo = activo.Id,
                        DepreciacionAcumulada = 0,
                        DepreciacionMes = 0,
                        valorResidual = activo.ValorActual,
                        Fecha = DateTime.Now
                    };



                    activo.Depreciacion.Add(depreciacion);

                    return repository.Save(activo,depreciacion);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


            

        }

        private Depreciacion ObtenerUltimaDepreciacionPorActivo(int id)
        {
            IRepositoryDepreciacion repository = new RepositoryDepreciacion();
            return repository.ObtenerUltimaDepreciacionPorActivo(id);
        }





    }
}
