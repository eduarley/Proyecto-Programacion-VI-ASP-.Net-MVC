using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{

    [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
    public class ActivoController : Controller
    {
        // GET: Activo
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            IEnumerable<Activo> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceActivo _ServiceActivo = new ServiceActivo();
                lista = _ServiceActivo.GetActivo();
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
            
            IServiceUsuario serviceUsuario = new ServiceUsuario();
            IServiceTipoActivo serviceTipoActivo = new ServiceTipoActivo();
            IServiceMarca serviceMarca = new ServiceMarca();
            IServiceVendedor serviceVendedor = new ServiceVendedor();
            IServiceAseguradora serviceAseguradora = new ServiceAseguradora();

            ViewBag.ListaUsuarios = serviceUsuario.GetUsuario();
            ViewBag.ListaTipoActivos = serviceTipoActivo.GetTipoActivo();
            ViewBag.ListaMarcas = serviceMarca.GetMarca();
            ViewBag.ListaVendedores = serviceVendedor.GetVendedor();
            ViewBag.ListaAseguradoras = serviceAseguradora.GetAseguradora();

            

            ServiceBCCR serviceBCCR = new ServiceBCCR();
            ViewBag.Compra = serviceBCCR.GetDolar(DateTime.Now, DateTime.Now, "c");
            ViewBag.Venta = serviceBCCR.GetDolar(DateTime.Now, DateTime.Now, "v");


            List<string> listaCondiciones = new List<string>()
            {
                "Excelente",
                "Bueno",
                "Regular",
                "Malo"
            };

            ViewBag.ListaCondiciones = listaCondiciones;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Activo activo)
        {
            string errores = "";

            

            try
            {


                if (Request.Files[0].ContentLength > 0)
                {
                    HttpPostedFileBase FileBaseActivo = Request.Files[0];
                    WebImage imageActivo = new WebImage(FileBaseActivo.InputStream);
                    activo.ImgActivo = imageActivo.GetBytes();
                }
                else
                {
                    IServiceActivo serviceActivo = new ServiceActivo();
                    Activo activoTemp = serviceActivo.GetActivoByID(activo.Id);
                    if (activoTemp != null)
                        if (activoTemp.ImgActivo != null) 
                            activo.ImgActivo = activoTemp.ImgActivo;
                }
                



                if (Request.Files[1].ContentLength > 0)
                {
                    HttpPostedFileBase FileBaseFactura = Request.Files[1];
                    WebImage imageFactura = new WebImage(FileBaseFactura.InputStream);
                    activo.ImgFactura = imageFactura.GetBytes();
                }
                else
                {
                    IServiceActivo serviceActivo = new ServiceActivo();
                    Activo activoTemp = serviceActivo.GetActivoByID(activo.Id);
                    if (activoTemp != null)
                        if (activoTemp.ImgFactura != null)
                            activo.ImgFactura = activoTemp.ImgFactura;
                }




                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceActivo _ServiceActivo = new ServiceActivo();
                    _ServiceActivo.Save(activo);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();

                    return RedirectToAction("Create", activo);
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
            ServiceActivo _ServiceActivo = new ServiceActivo();
            Activo activo = null;

            try
            {
                

                activo = _ServiceActivo.GetActivoByID(id);

                return View(activo);
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
            IServiceUsuario serviceUsuario = new ServiceUsuario();
            IServiceTipoActivo serviceTipoActivo = new ServiceTipoActivo();
            IServiceMarca serviceMarca = new ServiceMarca();
            IServiceVendedor serviceVendedor = new ServiceVendedor();
            IServiceAseguradora serviceAseguradora = new ServiceAseguradora();

            ViewBag.ListaUsuarios = serviceUsuario.GetUsuario();
            ViewBag.ListaTipoActivos = serviceTipoActivo.GetTipoActivo();
            ViewBag.ListaMarcas = serviceMarca.GetMarca();
            ViewBag.ListaVendedores = serviceVendedor.GetVendedor();
            ViewBag.ListaAseguradoras = serviceAseguradora.GetAseguradora();


            ServiceActivo _ServiceActivo = new ServiceActivo();
            Activo activo = null;




            ServiceBCCR serviceBCCR = new ServiceBCCR();
            ViewBag.Compra = serviceBCCR.GetDolar(DateTime.Now, DateTime.Now, "c");
            ViewBag.Venta = serviceBCCR.GetDolar(DateTime.Now, DateTime.Now, "v");


            List<string> listaCondiciones = new List<string>()
            {
                "Excelente",
                "Bueno",
                "Regular",
                "Malo"
            };
            ViewBag.ListaCondiciones = listaCondiciones;

            try
            {
               

                activo = _ServiceActivo.GetActivoByID(id);
                
                activo.CostoColon = (decimal)activo.ValorActual;    
                
               
                // Response.StatusCode = 500;
                return View(activo);
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
                

                ServiceActivo _ServiceActivo = new ServiceActivo();
                Activo activo = _ServiceActivo.GetActivoByID(id);

                return View(activo);
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
            ServiceActivo _ServiceActivo = new ServiceActivo();
            ServiceDepreciacion serviceDepreciacion = new ServiceDepreciacion();
            try
            {


                serviceDepreciacion.DeleteDepreciacion(id);
                _ServiceActivo.DeleteActivo(id);
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

        public ContentResult GetMarcaByName(string name)
        {
            IServiceMarca _ServiceMarca = new ServiceMarca();
            var lista = _ServiceMarca.GetMarcaByName(name).ToList();
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },
            };
            string json = JsonConvert.SerializeObject(lista, settings);

            return Content(json);
        }

    }
}