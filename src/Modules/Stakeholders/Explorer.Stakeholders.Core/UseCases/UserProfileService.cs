using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Messages;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class UserProfileService : BaseService<UserProfileDto, UserProfile>, IUserProfileService
{
    private readonly IMapper _mapper;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly ICrudRepository<User> _userRepository;
    private readonly ICrudRepository<Person> _personRepository;

    public UserProfileService(IUserProfileRepository userProfileRepository, ICrudRepository<User> userRepository, ICrudRepository<Person> personRepository, IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
        _userProfileRepository = userProfileRepository;
        _userRepository = userRepository;
        _personRepository = personRepository;
    }

    public Result<UserProfileDto> Update(long userId, UserProfileDto userProfileDto)
    {
        var user = _userRepository.Get(userId);
        var person = GetPersonByUserId(userId);

        if (user == null || person == null)
        {
            throw new Exception("User not found.");
        }

        UserProfile existingProfile = GetUserProfileByUserId(userId);

        if (existingProfile != null)
        {
            existingProfile.setBiography(userProfileDto.Biography);
            existingProfile.setMotto(userProfileDto.Motto);
            existingProfile.setProfileImage(Convert.FromBase64String(RemoveBase64Prefix(userProfileDto.ProfileImage ?? "")));

            _userProfileRepository.Update(existingProfile);
            UpdatePerson(userId, userProfileDto);

            return _mapper.Map<UserProfileDto>((existingProfile, person));
        }
        else
        {
            var userProfileToCreate = _mapper.Map<UserProfile>(userProfileDto);

            userProfileToCreate.setUserId(userId);
            _userProfileRepository.Create(userProfileToCreate);
            UpdatePerson(userId, userProfileDto);

            return _mapper.Map<UserProfileDto>((userProfileToCreate, person));
        }
    }

    public Result<UserProfileDto> Get(long id)
    {
        //UserProfile userProfile = null;
        //Person person = null;

        try
        {
            var userProfile = _userProfileRepository.Get(id);
            var person = GetPersonByUserId(id);

            if (userProfile != null && person != null)
            {
                var userProfileDto = _mapper.Map<UserProfileDto>((userProfile, person));

                userProfileDto.Messages = userProfile.ProfileMessages
                    .Select(m => new MessageDto(m.Id, m.SenderId, GetProfileDisplayName(m.SenderId) ?? "",
                    m.Content, m.SentAt, m.IsRead, new AttachmentDto())).ToList();

                return Result.Ok(userProfileDto);
            }


            var emptyProfile = new UserProfile(person!.UserId);
            return Result.Ok(_mapper.Map<UserProfileDto>((emptyProfile, person)));
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound).WithError($"Profile with id:{id} not found.");
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.Internal).WithError(e.Message);
        }
    }

    private string RemoveBase64Prefix(string base64)
    {
        var prefixIndex = base64.IndexOf(";base64,");
        return prefixIndex >= 0 ? base64.Substring(prefixIndex + 8) : base64;
    }

    private UserProfile GetUserProfileByUserId(long userId)
    {
        var pagedProfiles = _userProfileRepository.GetPaged(1, int.MaxValue);

        return pagedProfiles.Results.FirstOrDefault(profile => profile.UserId == userId);
    }

    private Person GetPersonByUserId(long userId)
    {
        var pagedPersons = _personRepository.GetPaged(1, int.MaxValue);

        return pagedPersons.Results.FirstOrDefault(person => person.UserId == userId);
    }

    private void UpdatePerson(long userId, UserProfileDto userProfileDto)
    {
        var person = GetPersonByUserId(userId);
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with UserId {userId} not found.");
        }

        Console.WriteLine($"Person found: {person.Id} - {person.Name} {person.Surname}");


        person.setName(userProfileDto.Name);
        person.setSurname(userProfileDto.Surname);
        _personRepository.Update(person);
    }

    private string? GetProfileDisplayName(long profileId)
    {
        try
        {
            var userProfile = _userProfileRepository.Get(profileId);

            if (userProfile == null) return null;

            var person = GetPersonByUserId(userProfile.UserId);
            var displayName = person.Name + " " + person.Surname;

            return displayName;
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}
