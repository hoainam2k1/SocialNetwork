using SocialNetwork.Contract.Abstractions.Message;
using SocialNetwork.Contract.Abstractions.Shared;
using SocialNetwork.Contract.Services.V1.Product;
using SocialNetwork.Domain.Abstractions.Repositories;
using SocialNetwork.Domain.Exceptions;

namespace SocialNetwork.Application.UserCases.V1.Commands.Product
{
    public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
    {
        private readonly IRepositoryBase<SocialNetwork.Domain.Entities.Product, Guid> _productRepository;

        public DeleteProductCommandHandler(IRepositoryBase<SocialNetwork.Domain.Entities.Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);
            product.Delete();
            _productRepository.Remove(product);

            return Result.Success();
        }
    }
}

