using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web;

namespace Infraestructure.Repository
{
    public class RepositoryDepreciacion : IRepositoryDepreciacion
    {
        public IEnumerable<Depreciacion> GetDepreciacionPorFecha(DateTime fecha)
        {
            try
            {

                IEnumerable<Depreciacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Depreciacion.Where(p => p.Fecha.Month == fecha.Month).ToList<Depreciacion>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Depreciacion> GetDepreciacionPorIdActivo(int id)
        {
            try
            {

                IEnumerable<Depreciacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Depreciacion.Where(p => p.IdActivo == id).ToList<Depreciacion>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Depreciacion ObtenerUltimaDepreciacionPorActivo(int id)
        {
            try
            {

                List<Depreciacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Depreciacion.Where(p => p.IdActivo == id).ToList<Depreciacion>();
                }
                return lista.LastOrDefault<Depreciacion>();
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
        
        public Depreciacion Save(Activo activo, Depreciacion depreciacion)
        {
            
            
            //if (depreciacion != null)
            //    if (depreciacion.Fecha.Month == DateTime.Now.Month)
            //        return null;



            int retorno = 0;
            try
            {
                if (depreciacion != null)
                {
                    
                    using (MyContext ctx = new MyContext())
                    {

                        ctx.Configuration.LazyLoadingEnabled = false;
                        ctx.Depreciacion.Add(depreciacion);

                        //se debe actualizar el valor actual del activo!!!!!!!
                        ctx.Activo.AddOrUpdate(activo);

                        retorno = ctx.SaveChanges();
                    }
                }
                else
                {
   
                    
                    activo.Depreciacion.Add(depreciacion);
                    
                    using (MyContext ctx = new MyContext())
                    {
                       

                        ctx.Configuration.LazyLoadingEnabled = false;
                        ctx.Depreciacion.Add(depreciacion);                       
                        retorno = ctx.SaveChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

            
            return depreciacion;
        }

        private List<Depreciacion> ObtenerDepreciacionesPorActivo(int id)
        {
            try
            {

                List<Depreciacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Depreciacion.Where(p => p.IdActivo == id).ToList<Depreciacion>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public void DeleteDepreciacion(int id)
        {
            int returno;
            try
            {
                List<Depreciacion> lista = ObtenerDepreciacionesPorActivo(id);
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    foreach (var item in lista)
                    {
                        ctx.Entry(item).State = EntityState.Deleted;
                    }

                    returno = ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
