﻿using SocialNetwork.Contract.Abstractions.Message;

namespace SocialNetwork.Contract.Services.V1.Product
{
    public static class DomainEvent
    {
        public record ProductCreated(Guid IdEvent, Guid Id, string Name, decimal Price, string Description) : IDomainEvent, ICommand;
        public record ProductDeleted(Guid IdEvent, Guid Id) : IDomainEvent, ICommand;
        public record ProductUpdated(Guid IdEvent, Guid Id, string Name, decimal Price, string Description) : IDomainEvent, ICommand;
    }
}
