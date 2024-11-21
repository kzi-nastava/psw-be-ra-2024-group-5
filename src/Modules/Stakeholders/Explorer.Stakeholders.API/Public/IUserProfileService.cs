using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IUserProfileService
{
    Result<UserProfileDto> Update(long userId,UserProfileDto userProfile);
    Result<UserProfileDto> Get(long userId);
    Result<List<UserProfileBasicDto>> GetBasicProfiles(List<long> userIds);
}
