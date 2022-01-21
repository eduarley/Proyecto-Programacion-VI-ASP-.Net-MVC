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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult List()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuario();

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
            IServiceRol _ServiceRol = new ServiceRol();
            ViewBag.ListaRol = _ServiceRol.GetRol();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public ActionResult Save(Usuario usuario)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceUsuario _ServiceUsuario = new ServiceUsuario();
                    _ServiceUsuario.Save(usuario);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return RedirectToAction("Create", usuario);
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

        public ActionResult Details(string id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario = null;

            


            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                usuario = _ServiceUsuario.GetUsuarioByID(id);
                
                return View(usuario);
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

        public ActionResult Edit(string id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario = null;

            IServiceRol _ServiceRol = new ServiceRol();
            ViewBag.ListaRol = _ServiceRol.GetRol();
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                usuario = _ServiceUsuario.GetUsuarioByID(id);
                usuario.PasswordHash = "";

                // Response.StatusCode = 500;
                return View(usuario);
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

        public ActionResult Delete(string id)
        {
            try
            {
                
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                ServiceUsuario _ServiceUsuario = new ServiceUsuario();
                Usuario usuario = _ServiceUsuario.GetUsuarioByID(id);

                
                return View(usuario);
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
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(string id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceUsuario.DeleteUsuario(id);
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