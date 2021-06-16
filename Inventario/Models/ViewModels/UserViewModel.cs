using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Models.ViewModels
{
    public class UserViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número es obligatorio")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage ="Solo se permiten números")]
        [StringLength(12, ErrorMessage = "El número es demasiado largo")]
        [Range(6, 9999999999, ErrorMessage = "0 a 9999999999")]
        [Display(Name = "Documento")]
        public string Documento { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener máximo 100 caracteres ", MinimumLength = 1)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener máximo 100 caracteres ", MinimumLength = 1)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "El {0} debe tener máximo 100 caracteres ", MinimumLength = 4)]
        [Display(Name = "Email")]
        public string Correo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage ="Debe seleccionar una categoría")]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }
    }

    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El número es obligatorio")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(12, ErrorMessage = "El número es demasiado largo")]
        [Range(6, 9999999999, ErrorMessage = "0 a 9999999999")]
        [Display(Name = "Documento")]
        public string Documento { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener máximo 100 caracteres ", MinimumLength = 1)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener máximo 100 caracteres ", MinimumLength = 1)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "El {0} debe tener máximo 100 caracteres ", MinimumLength = 4)]
        [Display(Name = "Email")]
        public string Correo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
        //[Required]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }
    }
}