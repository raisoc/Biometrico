using bo.gp.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace bo.gp.DB.Contexts
{
    public class DBContexto : DbContext
    {
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<FingerprintEvent> FingerprintEvents { get; set; }
     //   public DbSet<BS> BSs { get; set; }
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DispositivoConfiguration());
            modelBuilder.ApplyConfiguration(new FingerprintEventConfiguration());
            //modelBuilder.ApplyConfiguration(new BSConfiguration());
        }
    }
}
