using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.ViewModels;

namespace Web.Controllers
{
    [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
    public class DepreciacionController : Controller
    {
        // GET: Depreciacion
        public ActionResult Index()
        {
            IServiceActivo serviceActivo = new ServiceActivo();
            ViewBag.ListaActivos = serviceActivo.GetActivo();

            ViewModelParametroDepreciacion parametro = new ViewModelParametroDepreciacion();
            parametro.FechaCalculo = DateTime.Now;

            return View(parametro);
        }

       


        public ActionResult ErrorDepreciacion()
        {
            return View();
        }


        public ActionResult AjaxCalcularDepreciacion(ViewModelParametroDepreciacion parametro)
        {
          

            IServiceActivo serviceActivo = new ServiceActivo();
            IServiceDepreciacion serviceDepreciacion = new ServiceDepreciacion();
            Activo activo = null;
            Depreciacion depreciacion = null;
            try
            {

                activo = serviceActivo.GetActivoByID(parametro.IdActivo);
                depreciacion = serviceDepreciacion.Save(activo);

                if (depreciacion != null)
                {
                    if (depreciacion.valorResidual <= 1)
                    { //////////////////////// VALOR LLEGO A CERO//////////////////
                        TempData["Message"] = "El valor actual es cero. No se puede depreciar más";
                        TempData.Keep();
                        return RedirectToAction("ErrorDepreciacion");
                    }
                }

                if (depreciacion != null) 
                    return PartialView("_PartialViewDepreciacion");
                else
                { ////////////POR SI SE APLICA EN EL MISMO MES//////////////
                    TempData["Message"] = "No es posible aplicar la depreciación en el mismo mes." ;
                    TempData.Keep();
                    return RedirectToAction("ErrorDepreciacion");
                }
                    
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al aplicar la depreciación!" + ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
            
        }

    }
}