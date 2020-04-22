﻿// <auto-generated />
using Api4u.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api4u.Migrations
{
    [DbContext(typeof(ToonsContext))]
    [Migration("20200422203537_more_models")]
    partial class more_models
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Api4u.Models.Courses.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = "COMP2910",
                            InstructorId = 1,
                            Name = "Project Management"
                        },
                        new
                        {
                            CourseId = "COMP3973",
                            InstructorId = 2,
                            Name = "ASP.NET"
                        },
                        new
                        {
                            CourseId = "COMP3717",
                            InstructorId = 3,
                            Name = "Android"
                        },
                        new
                        {
                            CourseId = "COMP1536",
                            InstructorId = 4,
                            Name = "HTML & CSS"
                        });
                });

            modelBuilder.Entity("Api4u.Models.Courses.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstructorId");

                    b.ToTable("Instructors");

                    b.HasData(
                        new
                        {
                            InstructorId = 1,
                            Email = "zhang.liu@qq.com",
                            FirstName = "Zhang",
                            LastName = "Liu"
                        },
                        new
                        {
                            InstructorId = 2,
                            Email = "jin.ling@123.com",
                            FirstName = "Jin",
                            LastName = "Ling"
                        },
                        new
                        {
                            InstructorId = 3,
                            Email = "sam.sun@gmail.com",
                            FirstName = "Sam",
                            LastName = "Sun"
                        },
                        new
                        {
                            InstructorId = 4,
                            Email = "ann.fox@outlook.com",
                            FirstName = "Ann",
                            LastName = "Fox"
                        });
                });

            modelBuilder.Entity("Api4u.Models.Foods.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FoodCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("UnitPrice")
                        .HasColumnType("real");

                    b.HasKey("FoodId");

                    b.HasIndex("FoodCategoryId");

                    b.ToTable("Foods");

                    b.HasData(
                        new
                        {
                            FoodId = 1,
                            FoodCategoryId = 1,
                            Name = "Croissant",
                            Unit = "dozen",
                            UnitPrice = 9.98f
                        },
                        new
                        {
                            FoodId = 2,
                            FoodCategoryId = 3,
                            Name = "Carrots",
                            Unit = "lbs",
                            UnitPrice = 0.98f
                        },
                        new
                        {
                            FoodId = 3,
                            FoodCategoryId = 2,
                            Name = "Apples",
                            Unit = "lbs",
                            UnitPrice = 0.65f
                        });
                });

            modelBuilder.Entity("Api4u.Models.Foods.FoodCategory", b =>
                {
                    b.Property<int>("FoodCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodCategoryId");

                    b.ToTable("FoodCategories");

                    b.HasData(
                        new
                        {
                            FoodCategoryId = 1,
                            Description = @"Bakery products, which include bread, rolls, 
                cookies, pies, pastries, and muffins, are usually prepared from flour 
                or meal derived from some form of grain. Bread, already a common 
                staple in prehistoric times, provides many nutrients in the human diet.",
                            Name = "Bakery"
                        },
                        new
                        {
                            FoodCategoryId = 2,
                            Description = @"The sweet 
                and fleshy product of a tree or other plant that contains seed 
                and can be eaten as food.",
                            Name = "Fruit"
                        },
                        new
                        {
                            FoodCategoryId = 3,
                            Description = @"A plant 
                or part of a plant used as food, typically as accompaniment to meat 
                or fish, such as a cabbage, potato, carrot, or bean.",
                            Name = "Vegetables"
                        });
                });

            modelBuilder.Entity("Api4u.Models.Students.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Api4u.Models.Toons.People", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Votes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Api4u.Models.Vehicles.Vehicle", b =>
                {
                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Fuel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleManufacturerName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Model");

                    b.HasIndex("VehicleManufacturerName");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Model = "Corolla",
                            Fuel = "Gas",
                            Type = "Sedan",
                            VehicleManufacturerName = "Toyota"
                        },
                        new
                        {
                            Model = "Rav4",
                            Fuel = "Gas",
                            Type = "SUV",
                            VehicleManufacturerName = "Toyota"
                        },
                        new
                        {
                            Model = "Soul",
                            Fuel = "Electric",
                            Type = "SUV",
                            VehicleManufacturerName = "Kia"
                        },
                        new
                        {
                            Model = "Model S",
                            Fuel = "Electric",
                            Type = "Sedan",
                            VehicleManufacturerName = "Tesla"
                        });
                });

            modelBuilder.Entity("Api4u.Models.Vehicles.VehicleManufacturer", b =>
                {
                    b.Property<string>("VehicleManufacturerName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleManufacturerName");

                    b.ToTable("VehicleManufacturers");

                    b.HasData(
                        new
                        {
                            VehicleManufacturerName = "Toyota",
                            Country = "Japan"
                        },
                        new
                        {
                            VehicleManufacturerName = "Kia",
                            Country = "South Korea"
                        },
                        new
                        {
                            VehicleManufacturerName = "Renault",
                            Country = "France"
                        },
                        new
                        {
                            VehicleManufacturerName = "Mercedes",
                            Country = "Germany"
                        },
                        new
                        {
                            VehicleManufacturerName = "Tesla",
                            Country = "USA"
                        });
                });

            modelBuilder.Entity("Api4u.Models.Courses.Course", b =>
                {
                    b.HasOne("Api4u.Models.Courses.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api4u.Models.Foods.Food", b =>
                {
                    b.HasOne("Api4u.Models.Foods.FoodCategory", "FoodCategory")
                        .WithMany()
                        .HasForeignKey("FoodCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api4u.Models.Vehicles.Vehicle", b =>
                {
                    b.HasOne("Api4u.Models.Vehicles.VehicleManufacturer", "VehicleManufacturer")
                        .WithMany()
                        .HasForeignKey("VehicleManufacturerName");
                });
#pragma warning restore 612, 618
        }
    }
}
