using EksiSozlukClone.Projections.FavoriteServices;
using EksiSozlukClone.Projections.FavoriteServices.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<FavoriteService>();

var host = builder.Build();
host.Run();
