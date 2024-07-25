using Microsoft.EntityFrameworkCore;
using ProfessorWebsite.Dtos;
using ProfessorWebsite.Models;


namespace ProfessorWebsite.Data
{
    public class DataContext : DbContext
    {
       public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        
        }

        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Professor>(m =>
            {
                m.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(40);
            });

        }
    }
}
