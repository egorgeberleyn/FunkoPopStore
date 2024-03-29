using FunkoPopStore.Api;
using FunkoPopStore.Application;
using FunkoPopStore.Infrastructure;
using FunkoPopStore.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "Kitty Store API");
    });

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

#region Test setup

namespace FunkoPopStore.Api
{
    public partial class Program
    {
    }
} // so you can reference it from tests

#endregion