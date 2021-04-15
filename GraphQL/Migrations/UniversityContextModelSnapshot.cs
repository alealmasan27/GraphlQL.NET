﻿// <auto-generated />
using System;
using GraphQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GraphQLServer.Migrations
{
    [DbContext(typeof(UniversityContext))]
    partial class UniversityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GraphQLServer.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Credits = 10,
                            Title = "Object Oriented Programming 1"
                        });
                });

            modelBuilder.Entity("GraphQLServer.Models.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId", "CourseId")
                        .IsUnique();

                    b.ToTable("Enrollments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CourseId = 1,
                            Grade = 2,
                            StudentId = 1
                        },
                        new
                        {
                            Id = 2,
                            CourseId = 1,
                            Grade = 1,
                            StudentId = 2
                        },
                        new
                        {
                            Id = 3,
                            CourseId = 1,
                            StudentId = 3
                        });
                });

            modelBuilder.Entity("GraphQLServer.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EnrollmentDate = new DateTime(2021, 2, 24, 20, 41, 43, 268, DateTimeKind.Utc).AddTicks(4336),
                            FirstName = "Rafael",
                            LastName = "Foo"
                        },
                        new
                        {
                            Id = 2,
                            EnrollmentDate = new DateTime(2021, 2, 24, 20, 41, 43, 268, DateTimeKind.Utc).AddTicks(5117),
                            FirstName = "Pascal",
                            LastName = "Bar"
                        },
                        new
                        {
                            Id = 3,
                            EnrollmentDate = new DateTime(2021, 2, 24, 20, 41, 43, 268, DateTimeKind.Utc).AddTicks(5120),
                            FirstName = "Michael",
                            LastName = "Baz"
                        });
                });

            modelBuilder.Entity("GraphQLServer.Models.Enrollment", b =>
                {
                    b.HasOne("GraphQLServer.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraphQLServer.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("GraphQLServer.Models.Course", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("GraphQLServer.Models.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}