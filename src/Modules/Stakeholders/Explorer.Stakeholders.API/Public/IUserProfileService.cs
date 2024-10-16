using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IUserProfileService
{
    Result<UserProfileDto> Create(UserProfileDto userProfile);
    Result<UserProfileDto> Update(UserProfileDto userProfile);
    Result<UserProfileDto> Get(long userId);
}
