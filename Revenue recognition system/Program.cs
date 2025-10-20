using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Revenue_recognition_system.Data;
using Revenue_recognition_system.Domain.Repositories;
using Revenue_recognition_system.Repositories;
using Revenue_recognition_system.Services.Implementations;
using Revenue_recognition_system.Services.Interfaces;
using Revenue_recognition_system.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        // The project will scan the assembly, in which CompanyClientValidator is located, looking for other validators
        fv.RegisterValidatorsFromAssemblyContaining<AddCompanyClientValidator>(); 
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();