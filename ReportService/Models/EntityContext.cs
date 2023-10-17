using Microsoft.EntityFrameworkCore;
using PersonService.Models;

namespace ReportService.Models
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options) { }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportDetail> ReportDetails { get; set; }
    }
}