//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Depreciacion
    {
        public int IdActivo { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<decimal> valorResidual { get; set; }
        public int Consecutivo { get; set; }
        public Nullable<decimal> DepreciacionMes { get; set; }
        public Nullable<decimal> DepreciacionAcumulada { get; set; }
    
        public virtual Activo Activo { get; set; }
    }
}
