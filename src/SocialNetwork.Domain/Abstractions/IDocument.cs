﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialNetwork.Domain.Abstractions
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTimeOffset CreatedOnUtc { get; }

        DateTimeOffset? ModifiedOnUtc { get; }
    }
}