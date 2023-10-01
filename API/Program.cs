using API.Extentions;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddDomainServices();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var container = app.Services.CreateScope();
var userManager = container.ServiceProvider.GetRequiredService<UserManager<User>>();
var roleManager = container.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
if (!await roleManager.RoleExistsAsync("Admin"))
{
    var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
}

var user = await userManager.FindByEmailAsync("admin@admin.com");
if (user is null)
{
    user = new User
    {
        FirstName = "admin",
        LastName = "admin",
        UserName = "admin@admin.com",
        Email = "admin@admin.com",
        EmailConfirmed = true
    };
    var result = await userManager.CreateAsync(user, "Admin_2924");
    result = await userManager.AddToRoleAsync(user, "Admin");
}

app.Run();
