using System;
using System.Collections.Generic;
using GraphQLServer.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLServer
{
    public class UniversityContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(t => t.Enrollments)
                .WithOne(t => t.Student)
                .HasForeignKey(t => t.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasIndex(t => new { t.StudentId, t.CourseId })
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasMany(t => t.Enrollments)
                .WithOne(t => t.Course)
                .HasForeignKey(t => t.CourseId);

            var courses = new List<Course>
            {
                new() {Credits = 10, Title = "Object Oriented Programming 1", Id = 1}
            };
            var students = new List<Student>
            {
                new() {FirstName = "Rafael", LastName = "Foo", EnrollmentDate = DateTime.UtcNow, Id = 1},
                new() {FirstName = "Pascal", LastName = "Bar", EnrollmentDate = DateTime.UtcNow, Id = 2},
                new() {FirstName = "Michael", LastName = "Baz", EnrollmentDate = DateTime.UtcNow, Id = 3}
            };

            modelBuilder.Entity<Course>().HasData(courses);
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<Enrollment>().HasData(new List<Enrollment>
            {
                new()
                {
                    Id = 1,
                    CourseId = 1,
                    StudentId = 1,
                    Grade = Grade.C
                },
                new()
                {
                    Id = 2,
                    CourseId = 1,
                    StudentId = 2,
                    Grade = Grade.B
                },
                new()
                {
                    Id = 3,
                    CourseId = 1,
                    StudentId = 3
                }
            });
        }
    }
}