using EksiSozlukClone.Projections.VoteService;
using EksiSozlukClone.Projections.VoteService.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<VoteService>();

var host = builder.Build();
host.Run();
