using FluentResults;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IUserProfileServiceInternal
    {
        Result<byte[]?> GetProfileImageByUserId(long userId);
        Result<string> GetDisplayNameByUserId(long userId);
    }
}
