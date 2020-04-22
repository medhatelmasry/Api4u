dotnet aspnet-codegenerator controller -name InstructorsController -async -api -m Instructor -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name CoursesController -async -api -m Course -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name FoodCategoriesController -async -api -m FoodCategory -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name FoodsController -async -api -m Food -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name VehicleManufacturersController -async -api -m VehicleManufacturer -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name VehiclesController -async -api -m Vehicle -dc ToonsContext -outDir Controllers




