using SocialNetwork.Application.Abstractions;
using SocialNetwork.Contract.Abstractions.Message;
using SocialNetwork.Contract.Abstractions.Shared;
using SocialNetwork.Contract.Services.V1.Identity;
using System.Security.Claims;

namespace SocialNetwork.Application.UserCases.V1.Queries.Identity
{
    public class GetLoginQueryHandler : IQueryHandler<Query.Login, Response.Authenticated>
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ICacheService _cacheService;

        public GetLoginQueryHandler(IJwtTokenService jwtTokenService, ICacheService cacheService)
        {
            _jwtTokenService = jwtTokenService;
            _cacheService = cacheService;
        }

        public async Task<Result<Response.Authenticated>> Handle(Query.Login request, CancellationToken cancellationToken)
        {
            // Check User
            // Generate JWT Token
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, "Senior .NET Leader")
        };

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            var response = new Response.Authenticated()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddMinutes(5)
            };

            await _cacheService.SetAsync(request.Email, response);

            return  Result.Success(response);
        }
    }
}
