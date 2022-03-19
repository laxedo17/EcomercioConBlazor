using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editar { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
        //[NotMapped] evita que aparezan mapeadas nas taboas da base de datos

    }
}
