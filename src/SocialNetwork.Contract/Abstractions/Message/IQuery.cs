using MediatR;
using SocialNetwork.Contract.Abstractions.Shared;

namespace SocialNetwork.Contract.Abstractions.Message
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    { }
}
