using Microsoft.EntityFrameworkCore;
using TallerMecanico.Models.Domain.Entities;
using static TallerMecanico.Models.Domain.Config.EntityConfig;

namespace TallerMecanico.Models.Domain
{
    public class TallerMecanicoDBContext : DbContext
    {
        public TallerMecanicoDBContext(DbContextOptions<TallerMecanicoDBContext> options) : base(options) { }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; } 
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<AgrupadoModulos> AgrupadoModulos { get; set; }
        public DbSet<ModulosRoles> ModulosRoles { get; set; }
        public DbSet<Estado> Estados { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TallerMecanicoDBContext).Assembly);
            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            modelBuilder.ApplyConfiguration(new RolConfig());
            modelBuilder.ApplyConfiguration(new ModuloConfig());
            modelBuilder.ApplyConfiguration(new AgrupadoModulosConfig());
            modelBuilder.ApplyConfiguration(new ModulosRolesConfig());
            modelBuilder.ApplyConfiguration(new EstadoConfig());
        }
    }
}
