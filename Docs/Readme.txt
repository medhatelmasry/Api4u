dotnet-ef migrations add -o Migrations Athletes

dotnet-ef database update


dotnet aspnet-codegenerator controller -name InstructorsController -async -api -m Instructor -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name CoursesController -async -api -m Course -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name FoodCategoriesController -async -api -m FoodCategory -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name FoodsController -async -api -m Food -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name VehicleManufacturersController -async -api -m VehicleManufacturer -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name VehiclesController -async -api -m Vehicle -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name ContinentsController -async -api -m Continent -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name CitiesController -async -api -m City -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name CountriesController -async -api -m Country -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name ProvincessController -async -api -m Province -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name ActorsController -async -api -m Actor -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name MoviesController -async -api -m Movie -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name OrganismsController -async -api -m Organism -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name SpeciesController -async -api -m Specie -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name CompetitionsController -async -api -m Competition -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name AthletesController -async -api -m Athlete -dc ToonsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name TeamsController -async -api -m Team -dc ToonsContext -outDir Controllers
dotnet aspnet-codegenerator controller -name PlayersController -async -api -m Player -dc ToonsContext -outDir Controllers


