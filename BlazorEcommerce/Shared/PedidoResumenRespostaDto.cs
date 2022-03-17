using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class PedidoResumenRespostaDto
    {
        public int Id { get; set; }
        public DateTime PedidoDate { get; set; }
        public decimal PrecioTotal { get; set; } //non o mapearemos a unha base de datos co cal non necesitamos a notation Column aqui
        public string Producto { get; set; }
        public string ProductoImaxeUrl { get; set; }
    }
}
