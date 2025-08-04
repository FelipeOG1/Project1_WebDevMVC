using Proyecto1.Models;
using System.Runtime.Intrinsics;
using System.Text.Json.Serialization;
using Proyecto1.Data;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Repositories;
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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(@"Server=FELIPEPC\SQLEXPRESS;Database=LavadoCR;Trusted_Connection=True;TrustServerCertificate=True;")

);

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();

builder.Services.AddScoped<ILavadoRepository, LavadoRepository>();

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
