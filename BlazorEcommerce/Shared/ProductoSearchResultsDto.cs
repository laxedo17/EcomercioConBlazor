using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class ProductoSearchResultsDto
    {
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public int Paxinas { get; set; }
        public int PaxinaActual { get; set; }
    }
}
