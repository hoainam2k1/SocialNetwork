using MediatR;
using SocialNetwork.Contract.Abstractions.Shared;

namespace SocialNetwork.Contract.Abstractions.Message
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    { }
}
