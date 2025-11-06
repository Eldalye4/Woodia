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



app.MapControllers();
app.UseExceptionHandler();

app.Run();


// 3JV7BMH5QZXLKB2Z3FXZUKKN


// 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