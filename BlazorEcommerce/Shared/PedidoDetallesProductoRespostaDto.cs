using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class PedidoDetallesProductoRespostaDto
    {
        public int ProductoId { get; set; }
        public string Titulo { get; set; }
        public string ProductoType { get; set; }
        public string ImaxeUrl { get; set; }
        public int Cantidade { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
