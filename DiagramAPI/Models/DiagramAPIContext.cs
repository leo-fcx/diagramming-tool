using Microsoft.EntityFrameworkCore;

namespace DiagramAPI.Models
{
    public class DiagramAPIContext : DbContext
    {
        public DiagramAPIContext(DbContextOptions<DiagramAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Diagram> Diagrams { get; set; }
        public DbSet<Classifier> Classifiers { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diagram>()
                .HasMany(c => c.Classifiers)
                .WithOne(e => e.Diagram)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
