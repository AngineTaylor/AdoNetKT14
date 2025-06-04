using Microsoft.EntityFrameworkCore;
using EducationSystem.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Отношение один ко многим: Teacher -> Course
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Отношение многие ко многим: Student <-> Course
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentCourse",
                    j => j
                        .HasOne<Student>()
                        .WithMany()
                        .HasForeignKey("StudentId"),
                    j => j
                        .HasOne<Course>()
                        .WithMany()
                        .HasForeignKey("CourseId"),
                    j =>
                    {
                        j.HasKey("StudentId", "CourseId");
                    });
        }
    }
}