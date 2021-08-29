using DatabaseApp1.DbApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseApp1.DbApp.OtusContext
{
    internal class Context : DbContext
    {
        private readonly string _connectionString;

        public Context(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Course> Courses { get; set; }
        
        public DbSet<Lecture> Lectures { get; set; }
        
        public DbSet<Student> Students { get; set; }
        
        public DbSet<StudentCourse> StudentCourses { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecture>()
                .HasOne<Course>(x => x.CourseEntity)
                .WithMany(x => x.Lectures)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentCourse>(e =>
            {
                e.HasKey(x => new { x.StudentId, x.CourseId });
                
                e.HasOne(x => x.CourseEntity)
                    .WithMany(x => x.Students)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.StudentEntity)
                    .WithMany(x => x.Courses)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        
    }
}