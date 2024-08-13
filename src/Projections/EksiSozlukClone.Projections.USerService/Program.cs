using EksiSozlukClone.Projections.UserService;
using EksiSozlukClone.Projections.UserService.Services;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<UserService>();
        services.AddTransient<EmailService>();

    })
    .Build();

host.Run();
