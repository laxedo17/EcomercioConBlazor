using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Producto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty; //string.Empty para evitar mensaxe de advertencia "non-nullable property"
        public string ImaxeUrl { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; } //Column engadido para que Entity Framework recoñeza o valor decimal e os dixitos que levara por defecto
    }
}
