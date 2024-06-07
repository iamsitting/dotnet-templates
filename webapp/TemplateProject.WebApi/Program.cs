using Serilog;
using TemplateProject.Database;
using TemplateProject.Infrastructure;
using TemplateProject.WebApi;
using TemplateProject.WebApi.Htmx;
using TemplateProject.WebApi.React.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.AddCustomLogging(builder.Configuration);

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddReactApp();
builder.Services.AddHtmxApp();

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

app.MapControllers();
app.UseReactRoutes();
app.UseHtmxRoutes();

app.UseSerilogRequestLogging();

app.Run();
