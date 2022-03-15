global using BlazorEcommerce.Shared;
global using System.Net.Http.Json;
global using BlazorEcommerce.Client.Services.ProductoService;
global using BlazorEcommerce.Client.Services.CategoriaService;
global using Blazored.LocalStorage;
global using BlazorEcommerce.Client.Services.CarroService;
using BlazorEcommerce.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage(); //local storage engadido para usalo no carriño da compra
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>(); //igual que ProductoService, rexistramos con Dependency Injection
builder.Services.AddScoped<ICarroService, CarroService>();

await builder.Build().RunAsync();
