using ProjectKubRab.Worker;
using ProjectKubRab.Worker.Application.Abstractions;
using ProjectKubRab.Worker.Infrastructure.Client;
using ProjectKubRab.Worker.Infrastructure.Messaging;
using ProjectKubRab.Worker.Infrastructure.Messaging.Producers;
using ProjectKubRab.Worker.Infrastructure.Parsing;

var builder = Host.CreateApplicationBuilder(args);

#region RabbitMQ Config
builder.Services.AddSingleton<RabbitMQOptions>(opt =>
{
    var rabbitMQOptions = new RabbitMQOptions();
    builder.Configuration.GetSection("RabbitMQ").Bind(rabbitMQOptions);

    return rabbitMQOptions;
});
#endregion

builder.Services.AddHttpClient<IProductClient, ProductClient>(c =>
    c.BaseAddress = new Uri(builder.Configuration.GetSection("ProductApi:BaseUrl").Get<string>()??""));
builder.Services.AddSingleton<IProductHtmlExtractor, ProductHtmlExtractor>();
builder.Services.AddSingleton<IProductProducer, ProductProducer>();

builder.Services.AddHostedService(provider => new Worker(
    provider.GetRequiredService<ILogger<Worker>>(),
    provider.GetRequiredService<IHostApplicationLifetime>(),
    args,
    provider.GetRequiredService<IProductClient>(),
    provider.GetRequiredService<IProductHtmlExtractor>(),
    provider.GetRequiredService<IProductProducer>()));

builder.Services.Configure<HostOptions>(options =>
{
    // Automatically kills the process if a background service crashes
    options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.StopHost;
});

var host = builder.Build();

host.Run();
