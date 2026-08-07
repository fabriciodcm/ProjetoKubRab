namespace ProjectKubRab.API.Infrastructure.Persistence.Repositories
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
