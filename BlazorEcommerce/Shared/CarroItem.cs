using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class CarroItem
    {
        public int ProductoId { get; set; }
        public int ProductoTypeId { get; set; }
        public int Cantidade { get; set; } = 1;
    }
}
