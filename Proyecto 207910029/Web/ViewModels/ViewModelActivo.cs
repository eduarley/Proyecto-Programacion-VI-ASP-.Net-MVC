using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ViewModelActivo
    {
        public int Id { get; set; }      
        public string Descripcion { get; set; }
        public string Modelo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaCompra { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> VencimientoGarantia { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> VencimientoSeguro { get; set; }
        public Nullable<decimal> ValorActual { get; set; }
        public Nullable<decimal> CostoDolar { get; set; }
        public decimal CostoColon { get; set; }
        public string Condicion { get; set; }
    }
}