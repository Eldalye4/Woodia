using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependecy(builder.Configuration);
var app = builder.Build();
app.MapGet("/health-check", () => Results.Ok());
if (app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.Title = "ShopApi";
        opt.Theme = ScalarTheme.Solarized;
    });
}



app.UseSwagger();

// Admin Documentation
app.MapScalarApiReference("/scalar/admin", options =>
{
    options
        .WithTitle("Admin API Documentation")
        .WithTheme(ScalarTheme.Mars)
        .WithOpenApiRoutePattern("/swagger/admin/swagger.json");
});

// User Documentation
app.MapScalarApiReference("/scalar/client", options =>
{
    options
        .WithTitle("User API Documentation")
       .WithTheme(ScalarTheme.Purple)
        .WithOpenApiRoutePattern("/swagger/client/swagger.json");
});
app.MapScalarApiReference("/scalar/carpenter", options =>
{
    options
        .WithTitle("User API Documentation")
       .WithTheme(ScalarTheme.Purple)
        .WithOpenApiRoutePattern("/swagger/carpenter/swagger.json");
});

app.UseCors();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();


