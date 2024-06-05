using Serilog;
using TemplateProject.Database;
using TemplateProject.Infrastructure;
using TemplateProject.WebApi;
using TemplateProject.WebApi.React.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.AddCustomLogging(builder.Configuration);

builder.Services.AddCustomAuthentication(builder.Configuration);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddReactApp();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();
app.UseReactRoutes();
app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();
