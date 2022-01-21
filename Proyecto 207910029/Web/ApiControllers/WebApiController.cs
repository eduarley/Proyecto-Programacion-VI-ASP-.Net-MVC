using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Web.ViewModels;

namespace Web.ApiControllers
{
    public class WebApiController : ApiController
    {

       
        public IHttpActionResult GetActivo()
        {

            List<ViewModelActivo> listaViewmodel = new List<ViewModelActivo>();
            IEnumerable<Activo> lista = null;
            try
            {
                IServiceActivo _ServiceActivo = new ServiceActivo();
                lista = _ServiceActivo.GetActivo();
                //foreach (var item in lista)
                //{
                //    //item.Aseguradora = null;
                //    //item.TipoActivo = null;
                //    //item.Marca = null;
                //    //item.Vendedor = null;
                //    //item.Usuario = null;


                //}

                ViewModelActivo item = null;
                foreach (var activo in lista)
                {
                    item = new ViewModelActivo();
                    item.Id = activo.Id;
                    item.Descripcion = activo.Descripcion;
                    item.Modelo = activo.Modelo;
                    item.FechaCompra = activo.FechaCompra;
                    item.VencimientoGarantia = activo.VencimientoGarantia;
                    item.VencimientoSeguro = activo.VencimientoSeguro;
                    item.ValorActual = activo.ValorActual;
                    item.CostoColon = activo.CostoColon;
                    item.CostoDolar = activo.CostoDolar;
                    item.Condicion = activo.Condicion;

                    listaViewmodel.Add(item);
                }


                return Ok(listaViewmodel);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                // Redireccion a la captura del Error
                return Ok("Error");
            }
        }



    }
}
