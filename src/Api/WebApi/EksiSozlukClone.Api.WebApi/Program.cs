using EksiSozlukClone.Api.Application.Extensions;
using EksiSozlukClone.Infastructure.Persistence.Context;
using EksiSozlukClone.Infastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using EksiSozlukClone.Api.WebApi.Infastructure.ActionFilters.Extensions;
using EksiSozlukClone.Api.WebApi.Infastructure.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(opt => opt.Filters.Add<ValidateModelStateFilter>())
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddFluentValidation()
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationRegistration();
builder.Services.AddInfastructureRegistration(builder.Configuration);
builder.Services.ConfigureAuth(builder.Configuration);

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<EksiSozlukCloneDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("EksiSozlukCloneDbConnectionString"));
});

//Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptonHandler(app.Environment.IsDevelopment());

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyPolicy");
app.MapControllers();

app.Run();
