using ApplicationCore.Services;
using Infraestructure.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    [CustomAuthorize((int)Roles.Administrador, (int)Roles.Reportes)]
    public class ReporteController : Controller
    {



        public ActionResult ActivoCatalogo()
        {
            IEnumerable<Activo> lista = null;
            try
            {

                IServiceActivo _ServiceActivo = new ServiceActivo();
                lista = _ServiceActivo.GetActivo();
                return View(lista);
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
        }


        public ActionResult CreatePdfActivoCatalogo()
        {
            IEnumerable<Activo> lista = null;
            try
            {
                // Extraer informacion
                IServiceActivo _ServiceActivo = new ServiceActivo();
                lista = _ServiceActivo.GetActivo();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Catálogo de Activos")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 6 columnas 
                Table table = new Table(6, true);


                // los Encabezados
                table.AddHeaderCell("Id");
                table.AddHeaderCell("Descripcion");
                table.AddHeaderCell("Propietario");
                table.AddHeaderCell("Marca");
                table.AddHeaderCell("Valor Actual");
                table.AddHeaderCell("Imagen");


                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.Id.ToString()));
                    table.AddCell(new Paragraph(item.Descripcion.ToString()));
                    table.AddCell(new Paragraph(item.Usuario.Nombre+" "+ item.Usuario.Apellidos));
                    table.AddCell(new Paragraph(item.Marca.Descripcion.ToString()));
                    table.AddCell(new Paragraph(item.ValorActual.ToString()));

                    if (item.ImgActivo != null)
                    {
                        // Convierte la imagen que viene en Bytes en imagen para PDF
                        Image image = new Image(ImageDataFactory.Create(item.ImgActivo));
                        // Tamaño de la imagen
                        image = image.SetHeight(75).SetWidth(75);
                        table.AddCell(image);
                    }
                    else
                    {
                        table.AddCell(new Paragraph("Sin imagen"));
                    }
                    
                }
                doc.Add(table);

                // Calculo del monto total
                //decimal montoTotal = lista.ToList().Sum(k => k.Cantidad * k.Precio);
                decimal? montoTotal = lista.ToList().Sum(k => k.ValorActual);
                // Agrega  el monto total
                doc.Add(new Paragraph("\n\rMonto total " + montoTotal));


                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "Reporte Activos.pdf");

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

        }



        public ActionResult ReportePorCodigo()
        {
         
            return View();
        }


        public ActionResult AjaxReportePorCodigo(int id)
        {
           
            IServiceActivo serviceActivo = new ServiceActivo();
            IServiceDepreciacion serviceDepreciacion = new ServiceDepreciacion();
            Activo activo = null;
            try
            {


                activo = serviceActivo.GetActivoByID(id);
                if (activo != null)
                    activo.Depreciacion = (ICollection<Depreciacion>)serviceDepreciacion.GetDepreciacionPorIdActivo(activo.Id);


                return PartialView("_PartialViewReporteCodigo", activo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("ReportePorCodigo");
            }

           
        }



        public ActionResult ReporteVencimientoGarantia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxVencimientoGarantia(DateTime fechaInicio, DateTime fechaFin)
        {
            IServiceActivo serviceActivo = new ServiceActivo();
            IEnumerable<Activo> lista = new List<Activo>();


            try
            {

                lista = serviceActivo.GetActivosPorRangoFechas(fechaInicio, fechaFin);

                return PartialView("_PartialViewVencimientoGarantia",lista);

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("ReporteVencimientoGarantia");
            }


            
        }


       public ActionResult GraficoActivo()
        {
            IServiceActivo serviceActivo = new ServiceActivo();
            List<Activo> lista = serviceActivo.GetActivo().ToList<Activo>();
            string etiquetas = "";
            string precios = "";


            IServiceTipoActivo serviceTipoActivo = new ServiceTipoActivo();

            List<Activo> agrupados = new List<Activo>();



            var result = (from item in lista
                          group item by new { item.IdTipoActivo } into g
                          //where g.Count()>1
                          select new Activo()
                          {
                              
                              IdTipoActivo = g.Key.IdTipoActivo,
                              ValorActual = g.Sum(x=>x.ValorActual)
                          });



            if (lista.Count() > 0)
            {


                foreach (var item in result)
                {
                    item.TipoActivo = serviceTipoActivo.GetTipoActivoByID(item.IdTipoActivo);
                    etiquetas += "'" + item.TipoActivo.Descripcion + "',";
                    precios += item.ValorActual/*.ToString("##")*/ + ",";
                }


                etiquetas = etiquetas.Substring(0, etiquetas.Length - 1); // ultima coma
                precios = precios.Substring(0, precios.Length - 1);
                var colors = GenerateColors(lista.Count());
                // toma la lista y le agrega separa por comas (,)
                ViewBag.Colores = string.Join(",", colors.ToList());
                ViewBag.Etiquetas = etiquetas;
                ViewBag.Valores = precios;
                return View();
            }
            else
            {
            
                TempData["Message"] = "Error al procesar los datos! No existen registros de Activo ";
                TempData.Keep();
                return RedirectToAction("Default","Error");
            }

            
        }


        private List<string> GenerateColors(int pCantidad)
        {
            int numColors = pCantidad;
            var colors = new List<string>();
            var random = new Random(); // Make sure this is out of the loop!
            for (int i = 0; i < numColors; i++)
            {
                colors.Add(String.Format("'#{0:X6}'", random.Next(0x1000000)));
            }

            return colors;
        }

        public ActionResult ReporteCierreCalculo()
        {
            return View();
        }

        public ActionResult AjaxReporteCierreCalculo(DateTime fecha)
        {
            IServiceDepreciacion serviceDepreciacion = new ServiceDepreciacion();                
            try
            {

                var lista = serviceDepreciacion.GetDepreciacionPorFecha(fecha);

                return PartialView("_PartialViewReporteCierreCalculo",lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                @TempData["Action"] = "E";
                // Redireccion a la captura del Error
                return RedirectToAction("ReportePorCodigo");
            }
            
        }


        public ActionResult ReporteAnalitico()
        {
            return View();
        }


    }
}