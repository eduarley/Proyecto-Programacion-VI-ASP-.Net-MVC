using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{

    [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
    public class TipoActivoController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            IEnumerable<TipoActivo> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
                lista = _ServiceTipoActivo.GetTipoActivo();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error

                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        // [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(TipoActivo tipoActivo)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
                    _ServiceTipoActivo.Save(tipoActivo);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", tipoActivo);
                }

                // redirigir
                return RedirectToAction("List");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                ViewBag.Message = TempData["Message"];
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("List");
            }
        }

        // GET: Bodega/Details/5      

        public ActionResult Details(int id)
        {
            ServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
            TipoActivo tipoActivo = null;

            try
            {
                

                tipoActivo = _ServiceTipoActivo.GetTipoActivoByID(id);

                return View(tipoActivo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("List");
            }
        }

        // GET: Bodega/Edit/5

        public ActionResult Edit(int id)
        {
            ServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
            TipoActivo tipoActivo = null;
            try
            {


                tipoActivo = _ServiceTipoActivo.GetTipoActivoByID(id);


                // Response.StatusCode = 500;
                return View(tipoActivo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("List");
            }
        }


        // GET: Bodega/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                


                ServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();
                TipoActivo tipoActivo = _ServiceTipoActivo.GetTipoActivoByID(id);

                return View(tipoActivo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            ServiceTipoActivo _ServiceTipoActivo = new ServiceTipoActivo();

            try
            {

                


                _ServiceTipoActivo.DeleteTipoActivo(id);
                TempData["Action"] = "D";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("List");
            }
        }
    }
}