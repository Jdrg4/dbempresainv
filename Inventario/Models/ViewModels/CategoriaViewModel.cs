using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Models.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120, ErrorMessage ="El {0} debe tener al menos {1} caracteres",MinimumLength = 1)]
        [Display(Name ="Nombre Producto")]
        public string Nombre { get; set; }    
        [StringLength(120, ErrorMessage = "El {0} debe tener al menos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Descripción Producto")]
        public string Descripcion { get; set; }
    }

    public class EditCategoriaViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120, ErrorMessage = "El {0} debe tener al menos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Nombre Producto")]
        public string Nombre { get; set; }
        [StringLength(120, ErrorMessage = "El {0} debe tener al menos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Descripción Producto")]
        public string Descripcion { get; set; }
    }
}