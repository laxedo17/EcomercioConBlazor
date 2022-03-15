using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class CarroProductoRespostaDto
    {
        public int ProductoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int ProductoTypeId { get; set; }
        public string ProductoType { get; set; } = string.Empty;
        public string ImaxeUrl { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cantidade { get; set; }
    }
}
