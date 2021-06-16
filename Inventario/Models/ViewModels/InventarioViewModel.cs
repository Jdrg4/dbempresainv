using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Models.ViewModels
{
    public class InventarioViewModel
    {
        [Required]
        [StringLength(150, ErrorMessage = "El {0} debe tener máximo 150 caracteres", MinimumLength = 1)]
        [Display(Name ="Producto")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "El {0} debe tener máximo 150 caracteres", MinimumLength = 1)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Range(0,99999999, ErrorMessage = "Rango excededido, solo hasta 99999999")]
        //[RegularExpression()]
        public float Precio { get; set; }
        public string IdCategoria { get; set; }
        //public DateTime? fecha { get; set; }
    }

    public class EditInventarioViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "El {0} debe tener máximo 150 caracteres", MinimumLength = 1)]
        [Display(Name = "Producto")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "El {0} debe tener máximo 150 caracteres", MinimumLength = 1)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Range(0, 99999999, ErrorMessage = "Rango excededido, solo hasta 99999999")]
        //[RegularExpression()]
        public float Precio { get; set; }
        public int IdCategoria { get; set; }
    }
}

