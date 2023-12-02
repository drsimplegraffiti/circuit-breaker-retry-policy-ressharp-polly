##### Create a new sln
- mkdir baseApp
- cd baseApp
- dotnet new sln -n FormulaOne


##### Create a new webapi project
- dotnet new webapi -n FormulaOne.Api

##### Add the project to the solution
- dotnet sln FormulaOne.sln add FormulaOne.Api/FormulaOne.Api.csproj

##### Create a new class library Entities
- dotnet new classlib -n FormulaOne.Entities

##### Add the project to the solution
- dotnet sln FormulaOne.sln add FormulaOne.Entities/FormulaOne.Entities.csproj

##### Create a new class library DataService
- dotnet new classlib -n FormulaOne.DataService

##### Add the project to the solution
- dotnet sln FormulaOne.sln add FormulaOne.DataService/FormulaOne.DataService.csproj


##### Add package to DataService
➜  dotnet add FormulaOne.DataService package Microsoft.EntityFrameWorkCore --version 7.0.0
➜  dotnet add FormulaOne.DataService package Microsoft.EntityFrameWorkCore.Sqlite --version 7.0.0
➜  dotnet add FormulaOne.DataService package Microsoft.EntityFrameWorkCore.Tools --version 7.0.0
➜  dotnet add FormulaOne.DataService package Microsoft.EntityFrameWorkCore.Design --version 7.0.0

##### Add FormulaOne.Entities to FormulaOne.DataService
➜  dotnet add FormulaOne.DataService reference FormulaOne.Entities/FormulaOne.Entities.csproj
➜  dotnet add FormulaOne.DataService/ reference FormulaOne.Entities

##### Add Design to FormulaOne.Api
➜  dotnet add FormulaOne.Api package Microsoft.EntityFrameWorkCore.Design --version 7.0.0

##### Add FormulaOne.DataService to FormulaOne.Api
➜  dotnet add FormulaOne.Api reference FormulaOne.DataService/FormulaOne.DataService.csproj
➜  dotnet add FormulaOne.Api reference FormulaOne.DataService


##### Add migrations to FormulaOne.DataService
➜  dotnet ef migrations add InitialCreate --project FormulaOne.DataService/FormulaOne.DataService.csproj --startup-project FormulaOne.Api/FormulaOne.Api.csproj

or

➜  dotnet ef migrations add InitialCreate --project FormulaOne.DataService --startup-project FormulaOne.Api

or

cd FormulaOne.DataService
dotnet ef migrations add InitialCreate --startup-project ../FormulaOne.Api


##### Update database
➜  dotnet ef database update --project FormulaOne.DataService/FormulaOne.DataService.csproj --startup-project FormulaOne.Api/FormulaOne.Api.csproj

or

➜  dotnet ef database update --project FormulaOne.DataService --startup-project FormulaOne.Api

or

cd FormulaOne.DataService
dotnet ef database update --startup-project ../FormulaOne.Api


##### Add the AutoMapper package to FormulaOne.Api
➜  dotnet add FormulaOne.Api package AutoMapper --version 6.2.2
➜  dotnet add FormulaOne.Api package AutoMapper.Extensions.Microsoft.DependencyInjection --version 3.2.0

##### Add the FormulaOne.Entities package to FormulaOne.Api
➜  dotnet add FormulaOne.Api reference FormulaOne.Entities/FormulaOne.Entities.csproj


#