using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Direccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Apelidos { get; set; } = string.Empty;
        public string Rua { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string CP { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }
}
