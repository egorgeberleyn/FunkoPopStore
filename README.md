## API for the online pet store
### REST, Domain-Driven-Design, Clean Architecture  
###  Stack: 
ASP NET Core Web API, PostreSQL, EF Core, MediatR, Mapster, Redis, ErrorOr, Fluent API и Validation, Swagger, xUnit  
### Implemented: 
JWT bearer authentication, role-based authorization, command/query split, domain events, global error handling, model validation, object mapping, shopping cart(distributed cache), 
admin panel, unit-tests 
### Used Patterns:
CQRS, Repository

#### Admin Credentials
Email: ***admin123@gmail.com***<br>
Password: ***secret123***

#### How to Launch
1.  Install and launch Redis docker image 
```bash
docker run -p 6379:6379 --name redis-master -e REDIS_REPLICATION_MODE=master -e ALLOW_EMPTY_PASSWORD=yes bitnami/redis:latest
```
3. Change the database connection string in the appsettings.json file of the project
4. Start the project
5. Done!
