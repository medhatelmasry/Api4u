using Api4u.Models.Foods;
using Api4u.Models.Students;
using Api4u.Models.Toons;
using Api4u.Models.Vehicles;
using Microsoft.EntityFrameworkCore;
using Api4u.Models.Courses;
using Api4u.Models.Countries;
using Api4u.Models.Species;
using Api4u.Models.Movies;
using Api4u.Models.Athletics;
using System;
using Api4u.Models.Sports;

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

        public virtual DbSet<Continent> Continents { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Organism> Organisms { get; set; }
        public virtual DbSet<Specie> Species { get; set; }

        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<Athlete> Athletes { get; set; }

        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Player> Players { get; set; }

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

            #region "Seed Continent Data"
            modelBuilder.Entity<Continent>().HasData(
                new { ContinentName = "Africa" },
                new { ContinentName = "Asia" }, 
                new { ContinentName = "Europe" }, 
                new { ContinentName = "South America" }, 
                new { ContinentName = "Australia" }, 
                new { ContinentName = "North America" }
            );
            #endregion

            #region "Seed Country Data"
            // https://data.mongabay.com/igapo/world_statistics_by_area.htm
            modelBuilder.Entity<Country>().HasData(
                new { CountryName = "Canada", CapitalCity = "Ottawa", AreaSqKm = 	9976140, ContinentName="North America" },
                new { CountryName = "USA", CapitalCity = "Washington DC", AreaSqKm = 9629091, ContinentName="North America" },
                new { CountryName = "Brazil", CapitalCity = "Brasilia", AreaSqKm = 8511965, ContinentName="South America" },
                new { CountryName = "India", CapitalCity = "New Delhi", AreaSqKm = 3287590, ContinentName="Asia" },
                new { CountryName = "China", CapitalCity = "Beijing", AreaSqKm = 9596960, ContinentName="Asia" }
            );
            #endregion

            #region "Seed Province Data"
            modelBuilder.Entity<Province>().HasData(
                new { ProvinceId=1, Name = "Aalabama", CapitalCity = "Montgomery", CountryName="USA" },
                new { ProvinceId=2, Name = "California", CapitalCity = "Sacramento", CountryName="USA" },
                new { ProvinceId=3, Name = "Hawaii", CapitalCity = "Honolulu", CountryName="USA" },

                new { ProvinceId=4, Name = "Fujian", CapitalCity = "Fuzhou", CountryName="China" },
                new { ProvinceId=5, Name = "Hubei", CapitalCity = "Wuhan", CountryName="China" },

                new { ProvinceId=6, Name = "Ontario", CapitalCity = "Toronto", CountryName="Canada" },
                new { ProvinceId=7, Name = "Alberta", CapitalCity = "Edmopnton", CountryName="Canada" }
            );
            #endregion

            #region "Seed City Data"
            modelBuilder.Entity<City>().HasData(
                new { CityName = "Los Angeles", ProvinceId=2 },
                new { CityName = "San Francisco", ProvinceId=2 },
                new { CityName = "San Diego", ProvinceId=2 },

                new { CityName = "Xiamen", ProvinceId=4 },
                new { CityName = "Quanzhou", ProvinceId=4 },

                new { CityName = "Ottawa", ProvinceId=6 },
                new { CityName = "Windsor", ProvinceId=6 },
                new { CityName = "Kingston", ProvinceId=6 },
                new { CityName = "Mississauga", ProvinceId=6 },
                new { CityName = "Hamilton", ProvinceId=6 }
            );
            #endregion

            #region "Seed Specie Data"
            modelBuilder.Entity<Specie>().HasData(
                new { SpecieName = "Bird" },
                new { SpecieName = "Mammal" }, 
                new { SpecieName = "Insect" }, 
                new { SpecieName = "Reptile" }, 
                new { SpecieName = "Fish" }, 
                new { SpecieName = "Amphibians" }
            );
            #endregion

            #region "Seed Organism Data"
            modelBuilder.Entity<Organism>().HasData(
                new { OrganismId = 1, Name = "Australian brush turkey", SpecieName="Bird" },
                new { OrganismId = 2, Name = "Egyptian plover", SpecieName="Bird" }, 
                new { OrganismId = 3, Name = "White stork", SpecieName="Bird" }, 
                new { OrganismId = 4, Name = "Kingfisher", SpecieName="Bird" }, 
                new { OrganismId = 5, Name = "Atlantic salmon", SpecieName="Fish" }, 
                new { OrganismId = 6, Name = "Great white shark", SpecieName="Fish" }
            );
            #endregion

            #region "Seed Movie Data"
            modelBuilder.Entity<Movie>().HasData(
                new { MovieId = 1, Name = "Gandhi", DirectorFirstName="Richard", DirectorLastName="Attenborough", Year=1982, Rating="PG" },
                new { MovieId = 2, Name = "The Sound of Music", DirectorFirstName="Robert", DirectorLastName="Wise",Year=196, Rating="G" }, 
                new { MovieId = 3, Name = "My Fair Lady", DirectorFirstName="George", DirectorLastName="Cukor",Year=1964, Rating="PG" }, 
                new { MovieId = 4, Name = "The King and I", DirectorFirstName="Walter", DirectorLastName="Lang",Year=1956, Rating="G" }, 
                new { MovieId = 5, Name = "Chariots of Fire", DirectorFirstName="Hugh", DirectorLastName="Hudson",Year=1981, Rating="PG" }, 
                new { MovieId = 6, Name = "Spartacus", DirectorFirstName="Stanley", DirectorLastName="Kubrick",Year=1960, Rating="PG" }
            );
            #endregion

            #region "Seed Actor Data"
            modelBuilder.Entity<Actor>().HasData(
                new { ActorId = 1, FirstName = "Ben", LastName="Kingsley", MovieId=1 },
                new { ActorId = 2, FirstName = "John", LastName="Gielgud", MovieId=1 }, 
                new { ActorId = 3, FirstName = "Rohini", LastName="Hattangadi", MovieId=1 },
                new { ActorId = 4, FirstName = "Julie", LastName="Andrews", MovieId=2 }, 
                new { ActorId = 5, FirstName = "Christopher", LastName="Plummer", MovieId=2 }, 
                new { ActorId = 6, FirstName = "Audrey", LastName="Hepburn", MovieId=3 },
                new { ActorId = 7, FirstName = "Rex", LastName="Harrison", MovieId=3 }
            );
            #endregion

            #region "Seed Competition Data"
            // https://www.worldathletics.org/world-rankings/overall-ranking/men
            modelBuilder.Entity<Competition>().HasData(
                new { CompetitionId = 1, EventName = "Men's 100m" },
                new { CompetitionId = 2, EventName = "Men's Pole Vault" }, 
                new { CompetitionId = 3, EventName = "Men's 50km Race Walking" },
                new { CompetitionId = 4, EventName = "Women's 100m" }, 
                new { CompetitionId = 5, EventName = "Women's 100m" }, 
                new { CompetitionId = 6, EventName = "Women's Marathon" },
                new { CompetitionId = 7, EventName = "Men's Marathon" }
            );
            #endregion

            #region "Seed Athlete Data"
            modelBuilder.Entity<Athlete>().HasData(
                new { AthleteId = 1, FirstName = "Christian", LastName="Coleman",  
                    DateOfBirth=new DateTime(1996,3,6), Country="USA", CompetitionId=1
                },
                new { AthleteId = 2, FirstName = "Zharnel", LastName="Hughes",  
                    DateOfBirth=new DateTime(1995,7,13), Country="GBR", CompetitionId=1
                },
                new { AthleteId = 3, FirstName = "Brigid", LastName="Kosgei",  
                    DateOfBirth=new DateTime(1994,2,20), Country="KEN", CompetitionId=6
                },                
                new { AthleteId = 4, FirstName = "Worknesh", LastName="Degefa",  
                    DateOfBirth=new DateTime(1990,10,28), Country="ETH", CompetitionId=6
                },                 
                new { AthleteId = 5, FirstName = "Birhanu", LastName="Legese",  
                    DateOfBirth=new DateTime(1994,9,11), Country="ETH", CompetitionId=7
                },                 
                new { AthleteId = 6, FirstName = "Lawrence", LastName="Cherono",  
                    DateOfBirth=new DateTime(1988,8,7), Country="KEN", CompetitionId=7
                },                 
                new { AthleteId = 7, FirstName = "Armand", LastName="Duplantis",  
                    DateOfBirth=new DateTime(1999,11,10), Country="SWE", CompetitionId=2
                }                
            );
            #endregion

            #region "Seed Team Data"
            modelBuilder.Entity<Team>().HasData(
                new { 
                    TeamName="Canucks",
                    City="Vancouver",
                    Province="BC",
                    Country="Canada" 
                },
                new { 
                    TeamName="Sharks",
                    City="San Jose",
                    Province="CA",
                    Country="USA" 
                }, 
                new { 
                    TeamName="Oilers",
                    City="Edmonton",
                    Province="AB",
                    Country="Canada"
                },
                new { 
                    TeamName="Flames",
                    City="Calgary",
                    Province="AB",
                    Country="Canada"
                }, 
                new { 
                    TeamName="Ducks",
                    City="Anaheim",
                    Province="CA",
                    Country="USA"
                }, 
                new { 
                    TeamName="Lightening",
                    City="Tampa Bay",
                    Province="FL",
                    Country="USA"
                },
                new { 
                    TeamName="Blackhawks",
                    City="Chicago",
                    Province="IL",
                    Country="USA"
                }
            );
            #endregion

            #region "Seed Player Data"
            modelBuilder.Entity<Player>().HasData(
                new {
                    PlayerId = 1,
                    FirstName = "Bob",
                    LastName = "Fox",
                    TeamName = "Canucks",
                    Position = "Forward"
                },
                new { 
                    PlayerId = 2,
                    FirstName = "Sam",
                    LastName = "Dix",
                    TeamName = "Canucks",
                    Position = "Left Wing"
                }, 
                new {
                    PlayerId = 3,
                    FirstName = "John",
                    LastName = "Rooster",
                    TeamName = "Flames",
                    Position = "Right Wing" 
                },
                new { 
                    PlayerId = 4,
                    FirstName = "Pat",
                    LastName = "Plumber",
                    TeamName = "Oilers",
                    Position = "Defense" 
                }
            );
            #endregion

        }

    }

}