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


    [CustomAuthorize((int)Roles.Administrador)]
    public class AseguradoraController : Controller
    {
        // GET: Aseguradora
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            IEnumerable<Aseguradora> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
                lista = _ServiceAseguradora.GetAseguradora();
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
        public ActionResult Save(Aseguradora aseguradora)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
                    _ServiceAseguradora.Save(aseguradora);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return View("Create", aseguradora);
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
            ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
            Aseguradora aseguradora = null;

            try
            {


                aseguradora = _ServiceAseguradora.GetAseguradoraByID(id);

                return View(aseguradora);
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
            ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
            Aseguradora aseguradora = null;
            try
            {


                aseguradora = _ServiceAseguradora.GetAseguradoraByID(id);


                // Response.StatusCode = 500;
                return View(aseguradora);
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


                ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
                Aseguradora aseguradora = _ServiceAseguradora.GetAseguradoraByID(id);

                return View(aseguradora);
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
            ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();

            try
            {



                _ServiceAseguradora.DeleteAseguradora(id);
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