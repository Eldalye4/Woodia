using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependecy(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.Title = "ShopApi";
        opt.Theme = ScalarTheme.Solarized;
    });
}
if (app.Environment.IsDevelopment())
{
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
    app.MapScalarApiReference("/scalar/store", options =>
    {
        options
            .WithTitle("User API Documentation")
            .WithTheme(ScalarTheme.Purple)
            .WithOpenApiRoutePattern("/swagger/user/swagger.json");
    });
}


app.MapControllers();
app.UseExceptionHandler();

app.Run();
