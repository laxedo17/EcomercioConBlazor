﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class PedidoDetallesRespostaDto
    {
        public DateTime PedidoDate { get; set; }
        public decimal PrecioTotal { get; set; }
        public List<PedidoDetallesProductoRespostaDto> Productos { get; set; }
    }
}
