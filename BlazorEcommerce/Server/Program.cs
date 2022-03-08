global using BlazorEcommerce.Shared;//usamos global using para utilizar detalles do proxecto BlazorEcommerce.Shared desde aqui directamente
global using Microsoft.EntityFrameworkCore;
global using BlazorEcommerce.Server.Data;
using Microsoft.AspNetCore.ResponseCompression;

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


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
