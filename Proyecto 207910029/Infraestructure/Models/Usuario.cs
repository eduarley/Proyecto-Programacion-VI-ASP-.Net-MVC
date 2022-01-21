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

    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Activo = new HashSet<Activo>();
        }
        
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [MaxLength(9, ErrorMessage = "La {0} debe ser máximo {1} caracteres")]
        public string Id { get; set; }



        [Required(ErrorMessage = "{0} es un dato requerido")]
        [MaxLength(20, ErrorMessage = "El {0} debe ser máximo {1} caracteres")]
        public string Nombre { get; set; }




        [Required(ErrorMessage = "{0} es un dato requerido")]
        [MaxLength(40, ErrorMessage = "Los {0} debe ser máximo {1} caracteres")]
        public string Apellidos { get; set; }



        [Display(Name = "Rol")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdRol { get; set; }


        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30, ErrorMessage = "El {0} debe ser máximo {1} caracteres")]
        public string Email { get; set; }


        [Phone]
        [Display(Name = "Teléfono")]
        [MaxLength(8, ErrorMessage = "El {0} debe ser máximo {1} caracteres")]
        public string Telefono { get; set; }



        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.Password)]
        //[Range(minimum: 8, maximum: 10, ErrorMessage ="La {0} debe ser entre {1} y {2} caracteres")]
        public string PasswordHash { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activo> Activo { get; set; }
        public virtual Rol Rol { get; set; }


       

    }
}