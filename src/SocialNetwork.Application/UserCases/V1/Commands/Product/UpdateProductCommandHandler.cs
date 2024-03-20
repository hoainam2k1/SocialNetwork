﻿using SocialNetwork.Contract.Abstractions.Message;
using SocialNetwork.Contract.Abstractions.Shared;
using SocialNetwork.Contract.Services.V1.Product;
using SocialNetwork.Domain.Abstractions.Repositories;
using SocialNetwork.Domain.Exceptions;

namespace SocialNetwork.Application.UserCases.V1.Commands.Product
{
    public sealed class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProductCommand>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;

        public UpdateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result> Handle(Command.UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id); // Solution 1
            product.Update(request.Name, request.Price, request.Description);

            return Result.Success();
        }
    }
}

