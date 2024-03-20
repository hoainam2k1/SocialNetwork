using SocialNetwork.Contract.Abstractions.Message;
using SocialNetwork.Contract.Abstractions.Shared;
using SocialNetwork.Contract.Services.V1.Product;
using SocialNetwork.Domain.Abstractions.Repositories;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Exceptions;

namespace SocialNetwork.Application.UseCases.V1.Queries.Product
{
    public sealed class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductByIdQuery, Response.ProductResponse>
    {
        private readonly IMongoRepository<ProductProjection> _productRepository;

        public GetProductByIdQueryHandler(IMongoRepository<ProductProjection> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindOneAsync(p => p.DocumentId == request.Id)
                ?? throw new ProductException.ProductNotFoundException(request.Id);

            var result = new Response.ProductResponse(product.DocumentId, product.Name, product.Price, product.Description);

            return Result.Success(result);
        }
    }
}

