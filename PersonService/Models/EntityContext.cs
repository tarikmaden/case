using Microsoft.EntityFrameworkCore;

namespace PersonService.Models
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     // Global sorgu filtresi tanımlayın
        //     modelBuilder.HasPostgresExtension("uuid-ossp"); // PostgreSQL için gerekli

        //     foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //     {
        //         // Guid alanlarını tüm model sınıflarında otomatik oluştur
        //         if (entityType.ClrType.GetProperty("Id") != null &&
        //             entityType.ClrType.GetProperty("Id").PropertyType == typeof(Guid))
        //         {
        //             modelBuilder.Entity(entityType.ClrType).Property<Guid>("Id")
        //                 .HasDefaultValueSql("uuid_generate_v4()");
        //         }
        //     }
        // }
    }
}