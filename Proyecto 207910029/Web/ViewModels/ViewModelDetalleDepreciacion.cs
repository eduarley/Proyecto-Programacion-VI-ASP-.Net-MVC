using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ViewModelDetalleDepreciacion
    {
        public int Mes { get; set; }
        public double DepreciacionMes { get; set; }
        public double  DepreciacionAcumulada { get; set; }
        public double  ValorResidual { get; set; }
    }
}