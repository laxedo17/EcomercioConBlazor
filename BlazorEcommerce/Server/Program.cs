global using BlazorEcommerce.Shared;//usamos global using para utilizar detalles do proxecto BlazorEcommerce.Shared desde aqui directamente
global using Microsoft.EntityFrameworkCore;
global using BlazorEcommerce.Server.Data;
global using Microsoft.AspNetCore.ResponseCompression;
global using BlazorEcommerce.Server.Services.ProductoService;
global using BlazorEcommerce.Server.Services.CategoriaService;
global using BlazorEcommerce.Server.Services.CarroService;
global using BlazorEcommerce.Server.Services.AuthService;
global using BlazorEcommerce.Server.Services.PedidoService;
global using BlazorEcommerce.Server.Services.PagoService;
global using BlazorEcommerce.Server.Services.DireccionService;
global using BlazorEcommerce.Server.Services.ProductoTypeService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}); //preparando para facer uso de Entity Framework e SQL Server
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();//para engadir Swashbuckle (Swagger)
builder.Services.AddSwaggerGen(); //sumamos Swagger

builder.Services.AddScoped<IProductoService, ProductoService>();//tras usar Dependency Injection con IProductoService, queremos usar a clase ProductoService. Con Dependency Injection podemos facer probas con por exemplo unha clase ProductoService2 se queremos facer algún cambio e simplemente modificalo aquí nesta linha escribindo ProductoService2 en vez de ProductoService, é unha das ventaxas da Dependency Injection, tamen vale para facer Unit Testing moito mais facil
builder.Services.AddScoped<ICategoriaService, CategoriaService>(); //igual que con IProductoService pero con CategoriaService
builder.Services.AddScoped<ICarroService, CarroService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IDireccionService, DireccionService>();
builder.Services.AddScoped<IProductoTypeService, ProductoTypeService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddHttpContextAccessor(); //isto engadimolo para que vaia implicito no servicio e non tenhamos que chamar a userId no CarroController no metodo GardarItemsCarro e asi inxectamolo no constructor de CarroService

var app = builder.Build();

app.UseSwaggerUI(); //engadimos Swagger na variable app


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSwagger(); //e aqui debaixo tamen sumamos Swagger a proxecto, e podemos escribir /swagger/index.html para acceder a nosa API API
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

//despois de app.UseRouting, este orden e importante, usamos o authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

//plataforma de pago sintaxis
//stripe listen --forward-to https://localhost:7263/api/pago