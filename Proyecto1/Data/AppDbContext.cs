using Microsoft.EntityFrameworkCore;
using Proyecto1.Models;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace Proyecto1.Data
{
    public class AppDbContext : DbContext
    {
         public DbSet<Cliente> Clientes { get; set; }
         
         public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)


        {
        }

       

        
    
        
    
    




   }
   

}
