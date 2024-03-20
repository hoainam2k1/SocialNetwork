using SocialNetwork.Contract.Abstractions.Message;

namespace SocialNetwork.Contract.Services.V1.Identity
{
    public static class Command
    {
        public record Revoke(string AccessToken) : ICommand;
    }
}
