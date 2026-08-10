using MongoDB.Bson;
using MongoDB.Driver;
using ProjectKubRab.API.Core.Interfaces.Repositories;
using ProjectKubRab.API.Core.Interfaces.Services;
using ProjectKubRab.API.Core.Services;
using ProjectKubRab.API.Infrastructure.Messaging;
using ProjectKubRab.API.Infrastructure.Messaging.Consumers;
using ProjectKubRab.API.Infrastructure.Persistence.Repositories;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Mongo Config
builder.Services.AddSingleton<MongoDbOptions>(opt =>
{
    var mongoDbOptions = new MongoDbOptions();
    builder.Configuration.GetSection("MongoDB").Bind(mongoDbOptions);

    return mongoDbOptions;
});

builder.Services.AddSingleton<IMongoClient>(opt => { 
   var options = opt.GetRequiredService<MongoDbOptions>();
   return new MongoClient(options.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var options = serviceProvider.GetRequiredService<MongoDbOptions>();
    var client = serviceProvider.GetRequiredService<IMongoClient>();

    return client.GetDatabase(options.DatabaseName);
});

#endregion
#region RabbitMQ Config
builder.Services.AddSingleton<RabbitMQOptions>(opt =>
{
    var rabbitMQOptions = new RabbitMQOptions();
    builder.Configuration.GetSection("RabbitMQ").Bind(rabbitMQOptions);

    return rabbitMQOptions;
});

builder.Services.AddSingleton<IConnectionFactory>(opt =>
{
    var _rabbitMQOptions = opt.GetRequiredService<RabbitMQOptions>();
    var factory = new ConnectionFactory
    {
        HostName = _rabbitMQOptions.HostName,
        Port = _rabbitMQOptions.Port,
        UserName = _rabbitMQOptions.UserName,
        Password = _rabbitMQOptions.Password,
        AutomaticRecoveryEnabled = _rabbitMQOptions.AutomaticRecoveryEnabled
    };
    return factory;
});
#endregion

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddHostedService<ProductConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
