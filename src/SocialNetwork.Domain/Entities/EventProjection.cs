using SocialNetwork.Domain.Abstractions;
using SocialNetwork.Domain.Attributes;
using SocialNetwork.Domain.Constants;

namespace SocialNetwork.Domain.Entities
{
    [BsonCollection(CollectionNames.Event)]
    public class EventProjection : Document
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}

