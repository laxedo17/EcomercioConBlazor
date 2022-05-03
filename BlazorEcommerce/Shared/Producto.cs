using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Producto
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty; //string.Empty para evitar mensaxe de advertencia "non-nullable property"
        public string ImaxeUrl { get; set; } = string.Empty;
        //crea relacion de 1 a moitos para BD SQL Server con EF Core
        //1 Producto pode ter varias imaxes
        //que necesitaremos para facer migracion de BD e actualizala
        public List<Imaxe> Imaxes { get; set; } = new List<Imaxe>();

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal Precio { get; set; } 
        //Column engadido para que Entity Framework recoñeza o valor decimal e os dixitos que levara por defecto
        public Categoria? Categoria { get; set; }
        public int CategoriaId { get; set; } //poderia non ser necesaria porque Entity Framework agregara un Categorias Id pola sua conta, pero asi indicamoslle manualmente a Entity Framework
        //ademais cando fagamos algo de seed -xerminar- de datos vainos facer falta esta propiedade co cal poderemos establecer a id de categoria para un producto
        public bool Destacado { get; set; } //engadimos esta propiedade para que aparezan productos destacados na web e non todos de golpe
        public List<ProductoVariante> Variantes { get; set; } = new List<ProductoVariante>();
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editar { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
