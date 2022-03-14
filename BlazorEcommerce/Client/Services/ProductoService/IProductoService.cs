namespace BlazorEcommerce.Client.Services.ProductoService
{
    public interface IProductoService
    {
        List<Producto> Productos { get; set; }
        // Task GetProductos();
        Task GetProductos(string? categoriaUrl = null); //modificamos o metodo anterior e establecemos o valor a null, se non temos determinada categoria, igualmente desplegamos todos os productos da peticion de productos ao servidor
        Task<ServiceResposta<Producto>> GetProducto(int productoId);
        //Creamos un evento porque cando cargamos o component da lista de productos non se lanza o metodo OnInitializedAsync(), asi que os productos non se actualizarian nin recargarian con posibles cambios porque a app non sabe que algo cambiou
        //Con este evento faremos uso do metodo do metodo de lifecycle OnParametersSet()
        event Action ProductosCambiaron; //imos a Index.razor para implementar OnParametersSetAsync e activar o evento e desde ali imos ao component ProductoList.razor do proxecto Client
        string Mensaxe { get; set; } //para dar unha mensaxe que empezara no servicio para darlle informacion ao usuario nas busquedas solicitadas
        int PaxinaActual { get; set; }
        int ContaPaxinas { get; set; }
        string LastBusquedaText { get; set; }
        Task SearchProductos(string busquedaText, int paxina);
        Task<List<string>> GetProductoSearchSuxerencias(string busquedaText);
    }

}
