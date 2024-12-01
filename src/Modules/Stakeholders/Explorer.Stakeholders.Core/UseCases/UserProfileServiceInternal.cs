using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class UserProfileServiceInternal : IUserProfileServiceInternal
{
    private readonly IMapper _mapper;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;

    public UserProfileServiceInternal(IUserProfileRepository userProfileRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IPersonRepository personRepository)
    {
        _mapper = mapper;
        _userProfileRepository = userProfileRepository;
        _userRepository = userRepository;
        _personRepository = personRepository;
    }

    public Result<string> GetDisplayNameByUserId(long userId)
    {
        try
        {
            var person = _personRepository.GetByUserId(userId);

            if (person == null)
                return Result.Fail(FailureCode.NotFound).WithError("Person with not found.");

            return Result.Ok(person.GetDisplayName());
        }
        catch (Exception ex)
        {
            return Result.Fail(FailureCode.Internal).WithError(ex.Message);
        }
    }

    public Result<byte[]?> GetProfileImageByUserId(long userId)
    {
        try
        {
            var profile = _userProfileRepository.GetByUserId(userId);

            if (profile == null)
                return Result.Fail(FailureCode.NotFound).WithError("Profile not found.");

            return Result.Ok(profile.ProfileImage);
        }
        catch (Exception ex)
        {
            return Result.Fail(FailureCode.Internal).WithError(ex.Message);
        }
    }
}