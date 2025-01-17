﻿using SocialNetwork.Contract.Abstractions.Message;
using static SocialNetwork.Contract.Services.V1.Product.Response;

namespace SocialNetwork.Contract.Services.V1.Product
{
    public static class Query
    {
        public record GetProductsQuery() : IQuery<List<ProductResponse>>;
        public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
    }
}
