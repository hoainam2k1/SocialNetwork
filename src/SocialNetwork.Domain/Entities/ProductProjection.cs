using SocialNetwork.Domain.Abstractions;
using SocialNetwork.Domain.Attributes;
using SocialNetwork.Domain.Constants;

namespace SocialNetwork.Domain.Entities
{
    [BsonCollection(CollectionNames.Product)]
    public class ProductProjection : Document
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}

