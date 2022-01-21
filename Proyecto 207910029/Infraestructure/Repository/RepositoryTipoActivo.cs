using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web;

namespace Infraestructure.Repository
{
    public class RepositoryTipoActivo:IRepositoryTipoActivo
    {
        public void DeleteTipoActivo(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoActivo tipoActivo = new TipoActivo()
                    {
                        Id = id
                    };
                    ctx.Entry(tipoActivo).State = EntityState.Deleted;
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

        public IEnumerable<TipoActivo> GetTipoActivo()
        {
            try
            {
                // Forzar error
                // int x = 0;
                // x = 25 / x;


                IEnumerable<TipoActivo> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    //lista = ctx.Vendedor.Include("Producto").ToList<Vendedor>();
                    lista = ctx.TipoActivo.ToList<TipoActivo>();
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

        public TipoActivo GetTipoActivoByID(int id)
        {
            TipoActivo tipoActivo = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    tipoActivo = ctx.TipoActivo.Find(id);
                }

                return tipoActivo;
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

        public TipoActivo Save(TipoActivo tipoActivo)
        {
            int retorno = 0;
            TipoActivo oTipoActivo = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipoActivo = GetTipoActivoByID(tipoActivo.Id);
                    if (oTipoActivo == null)
                    {
                        ctx.TipoActivo.Add(tipoActivo);
                    }
                    else
                    {
                        ctx.Entry(tipoActivo).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();


                }

                if (retorno >= 0)
                    oTipoActivo = GetTipoActivoByID(tipoActivo.Id);

                return oTipoActivo;
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
