using Api4u.Models.Foods;
using Api4u.Models.Students;
using Api4u.Models.Toons;
using Api4u.Models.Vehicles;
using Microsoft.EntityFrameworkCore;
using Api4u.Models.Courses;

namespace Api4u.Data
{
    public partial class ToonsContext : DbContext
    {
        public ToonsContext()
        {
        }

        public ToonsContext(DbContextOptions<ToonsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<People> People { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<Food> Foods { get; set; }

        public virtual DbSet<VehicleManufacturer> VehicleManufacturers { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region "Seed Food Category Data"
            modelBuilder.Entity<FoodCategory>().HasData(
                new { FoodCategoryId = 1, Name = "Bakery", Description = @"Bakery products, which include bread, rolls, 
                cookies, pies, pastries, and muffins, are usually prepared from flour 
                or meal derived from some form of grain. Bread, already a common 
                staple in prehistoric times, provides many nutrients in the human diet." },

                new { FoodCategoryId = 2, Name = "Fruit", Description = @"The sweet 
                and fleshy product of a tree or other plant that contains seed 
                and can be eaten as food." },

                new { FoodCategoryId = 3, Name = "Vegetables", Description = @"A plant 
                or part of a plant used as food, typically as accompaniment to meat 
                or fish, such as a cabbage, potato, carrot, or bean." }
            );
            #endregion

            #region "Seed Food Data"
            modelBuilder.Entity<Food>().HasData(
                new { FoodId = 1, Name = "Croissant", Unit = "dozen", UnitPrice = 9.98f, FoodCategoryId = 1 },
                new { FoodId = 2, Name = "Carrots", Unit = "lbs", UnitPrice = 0.98f, FoodCategoryId = 3 },
                new { FoodId = 3, Name = "Apples", Unit = "lbs", UnitPrice = 0.65f, FoodCategoryId = 2 }
            );
            #endregion

            #region "Seed Vehicle Manufacturer Data"
            modelBuilder.Entity<VehicleManufacturer>().HasData(
                new { VehicleManufacturerName = "Toyota", Country = "Japan" },
                new { VehicleManufacturerName = "Kia", Country = "South Korea" },
                new { VehicleManufacturerName = "Renault", Country = "France" },
                new { VehicleManufacturerName = "Mercedes", Country = "Germany" },
                new { VehicleManufacturerName = "Tesla", Country = "USA" }
            );
            #endregion

            #region "Seed Vehicle Data"
            modelBuilder.Entity<Vehicle>().HasData(
                new { Model = "Corolla", Type = "Sedan", Fuel = "Gas", VehicleManufacturerName = "Toyota" },
                new { Model = "Rav4", Type = "SUV", Fuel = "Gas", VehicleManufacturerName = "Toyota" },
                new { Model = "Soul", Type = "SUV", Fuel = "Electric", VehicleManufacturerName = "Kia" },
                new { Model = "Model S", Type = "Sedan", Fuel = "Electric", VehicleManufacturerName = "Tesla" }
            );
            #endregion

            #region "Seed Instructor Data"
            modelBuilder.Entity<Instructor>().HasData(
                new { InstructorId = 1, FirstName = "Zhang", LastName = "Liu", Email = "zhang.liu@qq.com" },
                new { InstructorId = 2, FirstName = "Jin", LastName = "Ling", Email = "jin.ling@123.com" }, 
                new { InstructorId = 3, FirstName = "Sam", LastName = "Sun", Email = "sam.sun@gmail.com" }, 
                new { InstructorId = 4, FirstName = "Ann", LastName = "Fox", Email = "ann.fox@outlook.com" }
            );
            #endregion

            #region "Seed Course Data"
            modelBuilder.Entity<Course>().HasData(
                new { CourseId = "COMP2910", Name = "Project Management", InstructorId = 1 },
                new { CourseId = "COMP3973", Name = "ASP.NET", InstructorId = 2 },
                new { CourseId = "COMP3717", Name = "Android", InstructorId = 3 },
                new { CourseId = "COMP1536", Name = "HTML & CSS", InstructorId = 4 }
            );
            #endregion
        }
    }

}