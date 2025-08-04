using Microsoft.EntityFrameworkCore;
using Proyecto1.Models;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Proyecto1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Lavado> Lavados { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lavado>()
                .HasOne(l => l.Cliente)
                .WithMany()
                .HasForeignKey(l => l.ClienteIdentificacion)
                .HasPrincipalKey(c => c.Identificacion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lavado>()
                .HasOne(l => l.Vehiculo)
                .WithMany()
                .HasForeignKey(l => l.VehiculoPlaca)
                .HasPrincipalKey(v => v.Placa)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TipoLavado>().HasNoKey();

        }


    
   }
   

}
