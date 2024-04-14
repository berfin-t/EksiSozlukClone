using EksiSozlukClone.Api.Application.Extensions;
using EksiSozlukClone.Infastructure.Persistence.Context;
using EksiSozlukClone.Infastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationRegistration();
builder.Services.AddInfastructureRegistration(builder.Configuration);

builder.Services.AddDbContext<EksiSozlukCloneDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EksiSozlukCloneDbConnectionString"));
});


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

app.Run();
