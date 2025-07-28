using Proyecto1.Models;
using System.Runtime.Intrinsics;
using System.Text.Json.Serialization;
using Proyecto1.Repositiories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

        
    });



var app = builder.Build();

ClienteRepository.inicializarClientePorDefecto();
EmpleadoRepository.inicializarEmpleadoPorDefecto();
VehiculoRepository.inicializarVehiculoorDefecto();
LavadoRepository.inicialiarLavadoPorDefecto();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
