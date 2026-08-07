using ProjectKubRab.Worker;
using ProjectKubRab.Worker.Application.Abstractions;
using ProjectKubRab.Worker.Infrastructure.Client;
using ProjectKubRab.Worker.Infrastructure.Messaging.Producers;
using ProjectKubRab.Worker.Infrastructure.Parsing;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient<IProductClient, ProductClient>(c =>
    c.BaseAddress = new Uri("http://localhost:8585"));
builder.Services.AddSingleton<IProductHtmlExtractor, ProductHtmlExtractor>();
builder.Services.AddSingleton<IProductProducer, ProductProducer>();

builder.Services.AddHostedService(provider => new Worker(
    provider.GetRequiredService<ILogger<Worker>>(),
    args,
    provider.GetRequiredService<IProductClient>(),
    provider.GetRequiredService<IProductHtmlExtractor>(),
    provider.GetRequiredService<IProductProducer>()));

var host = builder.Build();
host.Run();
