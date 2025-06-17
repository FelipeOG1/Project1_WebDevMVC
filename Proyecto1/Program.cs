using Proyecto1.Models;
using System.Runtime.Intrinsics;

DateTime ahora=DateTime.Now;

Vehiculo ve = new Vehiculo(
    "3232dada",       // placa
    "Toyota",         // marca
    "Corolla",        // modelo
    "trasera",        // tracción
    "verde",          // color
    ahora,            // fecha
    true              // tratamiento
);


Vehiculo v2 = new Vehiculo(
    "3232dada",       // placa
    "Toyota",         // marca
    "Corolla",        // modelo
    "Delantera",        // tracción
    "verde",          // color
    ahora,            // fecha
    false              // tratamiento
);



Vehiculo.AgregarVehiculo(ve);


Vehiculo.AgregarVehiculo(v2);


 var vehiculos = Vehiculo.MostrarVehiculos();

 foreach(var vehiculo in vehiculos)
{

    Console.WriteLine(vehiculo); 


} 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
