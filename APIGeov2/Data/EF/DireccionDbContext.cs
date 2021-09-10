using API.Modelos;
using Microsoft.EntityFrameworkCore;

namespace API.Data.EF
{
    public class DireccionDbContext : DbContext
    {
        public virtual DbSet<Direccion> Direcciones { get; set; }

        public DireccionDbContext(DbContextOptions<DireccionDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Direccion_ConfiguracionEF());
        }
    }
}
