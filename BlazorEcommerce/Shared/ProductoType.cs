using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class ProductoType
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        [NotMapped]
        public bool Editar { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;

    }
}
