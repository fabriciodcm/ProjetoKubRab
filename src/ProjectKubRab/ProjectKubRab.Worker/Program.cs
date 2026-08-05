using ProjectKubRab.Worker;
using ProjectKubRab.Worker.Application.Abstractions;
using ProjectKubRab.Worker.Infrastructure.Client;
using ProjectKubRab.Worker.Infrastructure.Parsing;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient<IProductClient, ProductClient>(c =>
    c.BaseAddress = new Uri("http://localhost:8585"));
builder.Services.AddSingleton<IProductHtmlExtractor, ProductHtmlExtractor>();

builder.Services.AddHostedService(provider => new Worker(
    provider.GetRequiredService<ILogger<Worker>>(),
    args.Length > 0 ? args[0] : string.Empty,
    provider.GetRequiredService<IProductClient>(),
    provider.GetRequiredService<IProductHtmlExtractor>()));

var host = builder.Build();
host.Run();
