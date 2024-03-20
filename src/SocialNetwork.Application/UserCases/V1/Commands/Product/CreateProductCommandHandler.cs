using SocialNetwork.Contract.Abstractions.Message;
using SocialNetwork.Contract.Abstractions.Shared;
using SocialNetwork.Contract.Services.V1.Product;
using SocialNetwork.Domain.Abstractions.Repositories;

namespace SocialNetwork.Application.UserCases.V1.Commands.Product
{
    public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

        public CreateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), request.Name, request.Price, request.Description);
            _productRepository.Add(product);

            return Result.Success();
        }
    }
}

