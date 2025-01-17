﻿using MassTransit;

namespace SocialNetwork.Contract.Abstractions.Message
{
    [ExcludeFromTopology]
    public interface IDomainEvent
    {
        public Guid IdEvent { get; init; }
    }
}
