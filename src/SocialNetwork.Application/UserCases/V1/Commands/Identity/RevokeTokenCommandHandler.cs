using SocialNetwork.Application.Abstractions;
using SocialNetwork.Contract.Abstractions.Message;
using SocialNetwork.Contract.Abstractions.Shared;
using SocialNetwork.Contract.Services.V1.Identity;
using System.Security.Claims;


namespace SocialNetwork.Application.UserCases.V1.Commands.Identity
{
    public class RevokeTokenCommandHandler : ICommandHandler<Command.Revoke>
    {
        private readonly ICacheService _cacheService;
        private readonly IJwtTokenService _jwtTokenService;

        public RevokeTokenCommandHandler(ICacheService cacheService, IJwtTokenService jwtTokenService)
        {
            _cacheService = cacheService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<Result> Handle(Command.Revoke request, CancellationToken cancellationToken)
        {
            var AccessToken = request.AccessToken;
            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(AccessToken);
            var emailKey = principal.FindFirstValue(ClaimTypes.Email).ToString();

            var authenticated = await _cacheService.GetAsync<Response.Authenticated>(emailKey);
            if (authenticated is null)
                throw new Exception("Can not get value from Redis");

            await _cacheService.RemoveAsync(emailKey, cancellationToken);

            return Result.Success();
        }
    }
}
