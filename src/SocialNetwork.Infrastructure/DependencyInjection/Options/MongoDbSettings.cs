using SocialNetwork.Domain.Abstractions.Options;

namespace SocialNetwork.Infrastructure.DependencyInjection.Options
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }

}

