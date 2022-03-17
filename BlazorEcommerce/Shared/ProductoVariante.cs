using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class ProductoVariante
    {
        //non usaremos Id aqui, senon que usaremos unha clave composite, -composta- de unha Id de Producto e a Id de ProductoType
        //Usamos JsonIgnore para evitar referencias cruzadas, porque en cada producto con variantes do server teremos un tipo de Producto que por suposto pode ter variantes, etc etc, o cal crea referencias circulares
        //TODO: Este problema solucionase con DTOs -Data Transfer Objects-
        [JsonIgnore]
        public Producto Producto { get; set; }
        public int ProductoId { get; set; }
        public ProductoType ProductoType { get; set; }
        public int ProductoTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrixinalPrecio { get; set; } //Por si usamos un desconto
        
    }
}
