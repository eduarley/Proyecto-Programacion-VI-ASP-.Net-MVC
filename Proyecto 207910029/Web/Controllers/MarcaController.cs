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
    public class MarcaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            IEnumerable<Marca> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceMarca _ServiceMarca = new ServiceMarca();
                lista = _ServiceMarca.GetMarca();
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
        public ActionResult Save(Marca marca)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceMarca _ServiceMarca = new ServiceMarca();
                    _ServiceMarca.Save(marca);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", marca);
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
            ServiceMarca _ServiceMarca = new ServiceMarca();
            Marca marca = null;

            try
            {
               

                marca = _ServiceMarca.GetMarcaByID(id);

                return View(marca);
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
            ServiceMarca _ServiceMarca = new ServiceMarca();
            Marca marca = null;
            try
            {
              

                marca = _ServiceMarca.GetMarcaByID(id);


                // Response.StatusCode = 500;
                return View(marca);
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
                

                ServiceMarca _ServiceMarca = new ServiceMarca();
                Marca marca = _ServiceMarca.GetMarcaByID(id);

                return View(marca);
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
            ServiceMarca _ServiceMarca = new ServiceMarca();

            try
            {

                

                _ServiceMarca.DeleteMarca(id);
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