# Vrhw
Diff API - Assignment from Teletrax / 4C Insights

## Getting Started
Follow the next steps to setup the project in you local machine:
* Get the solution and open it with Visual Studio.
* Restore the NuGet Packages and Build the solution.
* By default the application uses a memory repository so there is no need to worry about setting up a storage.
* Make sure that ```Vrhw.Api``` is set as default startUp project and run the application.

The application should be up and running and ready to be tested with your favorite api testing tool.

If you're curious, Vrhw stands for Vladimir Rivera Homework.

#### Setting up the Sql Repository (Sql Server is required because of the connection string)
* Open the NuGet Package Manager Console. Tools > NuGet Package Manager > Package Manager Console.
* Make sure that ```Vrhw.Api``` is the default startUp project
* In the Package Mnager control in the Default project dropdown select ```Vrhw.Repository.Sql```
* Type ```Update-Database``` and press enter.
```
PM> Update-Database
```
* Edit the ```Vrhw.Api/App_Start/SimpleInjectorInitializer.cs``` to instantiate the ```SqlRepository``` instead of the 
```MemoryRepository```. The line is there so just comment out the ```MemoryRepository``` line and uncomment 
the ```SqlRepository```.
```
container.Register<IDiffRepository, SqlRepository>(Lifestyle.Scoped);
//container.Register<IDiffRepository, MemoryRepository>(Lifestyle.Scoped);
```
Now you can run the ```Vrhw.Api``` and it will use a Sql Server Database as Repository.
* In order to run the integration tests it is necesary to edit the ```Vrhw.Api/IntegrationTests/Startup.cs``` to instantiate the 
```SqlRepository``` instead of the ```MemoryRepository```.

#### Running Unit Tests and Integration Tests
The project includes the xunit runner for visual studio, so they can be executed with the VS Test explorer.
* Go to Test > Windows > Test Explorer.
* In the Test Explorer Window Click Run All.

## Projects in solution

#### Vrhw.Core
Here is where the Business Logic lays.

#### Vrhw.Api
Exposes the endpoints to interact with the Business Logic.

#### Vrhw.Shared
Contains Interfaces, Dtos, Models and other elements that are shared across the projects of the solution.

#### Vrhw.Tests
Contains the Unit tests and integration tests.

### Repositories
In order to provide options when it comes to define the data repository, there are two repositores to choose.
This demostrates the god practices of not coupling the code to a specific storage.

#### Vrhw.Repository.Memory
A memory repository.

#### Vrhm.Repository.Sql
A Sql Server repository using Entity Framework and Code First Migrations.

## Tecnical Stack
* Visual Studio 2015
* ASP.NET Web API 2
* Entity Framework 6 Code First Migrations
* SimpleInjector
* xunit
* Moq
* FluentAssertions
* OWIN

