using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.Models.TableViewModels
{
    public class InventarioTableViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string IdCategoria { get; set; }
        public float Precio { get; set; }
        public DateTime? Fecha { get; set; }
    }
}