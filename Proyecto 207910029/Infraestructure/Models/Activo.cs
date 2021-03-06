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
    using System.ComponentModel.DataAnnotations;

    public partial class Activo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activo()
        {
            this.Depreciacion = new HashSet<Depreciacion>();
        }
        
        [Display(Name ="Código")]
        [Required(ErrorMessage ="{0} es un dato requerido")]
        public int Id { get; set; }



        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string IdUsuario { get; set; }



        public int Serie { get; set; }



        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [MaxLength(50, ErrorMessage = "La {0} debe ser máximo {1} caracteres")]
        public string Descripcion { get; set; }


        [Display(Name = "Aseguradora")]
        public Nullable<int> IdAseguradora { get; set; }


        [Display(Name = "Tipo Activo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdTipoActivo { get; set; }



        [Display(Name = "Marca")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdMarca { get; set; }




        [Display(Name = "Cédula Jurídica del Vendedor")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [MaxLength(50, ErrorMessage = "La {0} debe ser máximo {1} caracteres")]
        public string CedJurVendedor { get; set; }



        [MaxLength(50, ErrorMessage = "El {0} debe ser máximo {1} caracteres")]
        public string Modelo { get; set; }




        [Display(Name = "Fecha de Compra")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaCompra { get; set; }





        [Display(Name = "Vencimiento Garantía")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> VencimientoGarantia { get; set; }





        [Display(Name = "Vencimiento del Seguro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> VencimientoSeguro { get; set; }





        [Display(Name = "Valor Actual")]
        public Nullable<decimal> ValorActual { get; set; }




        [Display(Name = "Costo Dólar")]
        public Nullable<decimal> CostoDolar { get; set; }





        [Display(Name = "Costo Colón")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal CostoColon { get; set; }




        [Display(Name = "Condición")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Condicion { get; set; }





        [Display(Name = "Imagen Factura")]
        public byte[] ImgFactura { get; set; }




        [Display(Name = "Imagen Activo")]
        public byte[] ImgActivo { get; set; }





    
        public virtual Aseguradora Aseguradora { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual TipoActivo TipoActivo { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Vendedor Vendedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Depreciacion> Depreciacion { get; set; }
    }
}
