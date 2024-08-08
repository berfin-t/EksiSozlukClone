using EksiSozlukClone.Projections.UserService.Services;
using EksiSozlukClone.Projections.USerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<UserService>();
        services.AddTransient<EmailService>();

    })
    .Build();

host.Run();
