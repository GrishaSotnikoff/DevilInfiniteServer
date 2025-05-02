// ====================================
// Server: ASP.NET Core MVC Controllers with Swagger
// File: Program.cs
// ====================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameLauncher API V1");
        c.RoutePrefix = string.Empty;
    });

// Serve static files from wwwroot
var fileProvider = new PhysicalFileProvider(
    Path.Combine(AppContext.BaseDirectory, "wwwroot")
);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = string.Empty,
    ServeUnknownFileTypes = true
});

app.UseRouting();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

app.Run();
