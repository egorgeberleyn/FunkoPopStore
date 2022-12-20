## API for the online pet store
### REST, Domain-Driven-Design, Clean Architecture  
###  Stack: 
ASP NET Core Web API, PostreSQL, EF Core, MediatR, Mapster, Redis, ErrorOr, Fluent API и Validation, Swagger, xUnit  
### Implemented: 
JWT bearer authentication, role-based authorization, command/query split, global error handling, model validation, object mapping, shopping cart(distributed cache), 
admin panel, unit-tests 
### Used Patterns:
CQRS, Repository,Mediator

#### Admin Credentials
Email: ***admin123@gmail.com***<br>
Password: ***secret123***

#### How Launch
1. Install or enable WSL2 (to install Redis on Windows, you'll first need to enable Windows Subsystem for Linux)
2. Download Redis zip archive (https://github.com/microsoftarchive/redis/releases)
   Unzip to a needed folder and run the redis-server.exe file
3. Change the database connection string in the appsettings.json file of the project
4. Start the project
5. Done!

