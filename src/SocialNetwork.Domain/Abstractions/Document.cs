using MongoDB.Bson;

namespace SocialNetwork.Domain.Abstractions
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public Guid DocumentId { get; set; } // Id cua SourceMessage: ProductID, CustomerID, OrderID

        public DateTimeOffset CreatedOnUtc => Id.CreationTime;

        public DateTimeOffset? ModifiedOnUtc { get; set; }
    }
}

