using EksiSozlukClone.Projections.VoteService;
using EksiSozlukClone.Projections.VoteService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<VoteService>();
    })
    .Build();

host.Run();
