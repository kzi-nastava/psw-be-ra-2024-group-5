using System;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class UserProfileService : BaseService<UserProfileDto, UserProfile>, IUserProfileService
{
    private readonly IMapper _mapper;
    private readonly ICrudRepository<UserProfile> _userProfileRepository;
    private readonly ICrudRepository<User> _userRepository;
    private readonly ICrudRepository<Person> _personRepository;
    public UserProfileService(ICrudRepository<UserProfile> userProfileRepository, ICrudRepository<User> userRepository, ICrudRepository<Person> personRepository, IMapper mapper) : base(mapper)
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
            existingProfile.setProfilePictureUrl(userProfileDto.ProfilePictureUrl);
            existingProfile.setBiography(userProfileDto.Biography);
            existingProfile.setMotto(userProfileDto.Motto);

            _userProfileRepository.Update(existingProfile);
            UpdatePerson(userId, userProfileDto);
            return _mapper.Map<UserProfileDto>((existingProfile, person));
        } else 
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
        UserProfile userProfile = null;
        Person person = null;

        try
        {
            userProfile = _userProfileRepository.Get(id);
        }
        catch (KeyNotFoundException)
        {
            userProfile = null;
        }

        if (userProfile != null)
        {
            person = GetPersonByUserId(userProfile.UserId);
            return Result.Ok(_mapper.Map<UserProfileDto>((userProfile, person)));
        }

        person = GetPersonByUserId(id);
        if (person == null)
        {
            return Result.Fail<UserProfileDto>("Profile not found.");
        }

        var emptyProfile = new UserProfile();
        emptyProfile.setUserId(person.UserId);
        emptyProfile.setBiography(string.Empty);
        emptyProfile.setMotto(string.Empty);

        return Result.Ok(_mapper.Map<UserProfileDto>((emptyProfile, person)));
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
        _personRepository.Update(person); //ovdje pada test - exception key not found iako -11 id postoji
    }
}
