using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    /// <summary>
    /// Clase para gardar cada elemento dun pedido, que despois pasara a ser unha lista no pedido completo
    /// </summary>
    public class PedidoProducto
    {
        public Pedido Pedido { get; set; }
        public int PedidoId { get; set; }
        public Producto Producto { get; set; }
        public int ProductoId { get; set; }
        public ProductoType ProductoType { get; set; }
        public int ProductoTypeId { get; set; }
        public int Cantidade { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioTotal { get; set; }
    }
}
