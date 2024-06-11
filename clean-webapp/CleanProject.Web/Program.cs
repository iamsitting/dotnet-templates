using CleanProject.Infrastructure;
using CleanProject.Persistence;
using CleanProject.Presentation.React.Extensions;
using CleanProject.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration, isLocal:true);
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddReactApp();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();
app.UseReactRoutes();
app.Map("/", () => "Hello!");
app.Run("http://localhost:5140");
