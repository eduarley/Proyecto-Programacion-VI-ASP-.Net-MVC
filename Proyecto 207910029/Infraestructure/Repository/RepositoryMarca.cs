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
    public class RepositoryMarca : IRepositoryMarca
    {
        public void DeleteMarca(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Marca marca = new Marca()
                    {
                        Id = id
                    };
                    ctx.Entry(marca).State = EntityState.Deleted;
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

        public IEnumerable<Marca> GetMarca()
        {
            try
            {
                // Forzar error
                // int x = 0;
                // x = 25 / x;


                IEnumerable<Marca> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    //lista = ctx.Vendedor.Include("Producto").ToList<Vendedor>();
                    lista = ctx.Marca.ToList<Marca>();
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

        public Marca GetMarcaByID(int id)
        {
            Marca marca = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    marca = ctx.Marca.Find(id);
                }

                return marca;
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

        public Marca Save(Marca marca)
        {
            int retorno = 0;
            Marca oMarca = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMarca = GetMarcaByID(marca.Id);
                    if (oMarca == null)
                    {
                        ctx.Marca.Add(marca);
                    }
                    else
                    {
                        ctx.Entry(marca).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();


                }

                if (retorno >= 0)
                    oMarca = GetMarcaByID(marca.Id);

                return oMarca;
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
        public IEnumerable<Marca> GetMarcaByName(string name)
        {
            IEnumerable<Marca> lista = null;

            string sql =
                string.Format("select * from Marca where descripcion like  '%{0}%' ", name);
            using (MyContext ctx = new MyContext())
            {
                lista = ctx.Marca.SqlQuery(sql).ToList<Marca>();
            }

            return lista;
        }
    }
}
