using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    /// <summary>
    /// (Paso 20) Devolve unha resposta sobre o servicio que presta a clase para non ter todo nun controller. Facemolo generic para aceptar calquera tipo de obxeto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResposta<T>
    {
        public T? Data { get; set; } //con generics creamos este campo nullable que sera a nosa lista de productos
        public bool Exito { get; set; } = true; //establecido valor en true por defecto
        public string Mensaxe { get; set; } = string.Empty;

    }
}
