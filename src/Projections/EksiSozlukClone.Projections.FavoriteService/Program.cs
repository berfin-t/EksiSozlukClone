using EksiSozlukClone.Projections.FavoriteService;
using EksiSozlukClone.Projections.FavoriteService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddTransient<FavoriteService>();

    })
    .Build();

host.Run();
