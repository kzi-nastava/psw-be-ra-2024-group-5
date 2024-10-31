using System;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;


namespace Explorer.Stakeholders.Core.UseCases;

public class FollowerService : BaseService<FollowerDto, Follower>, IFollowerService
{
    private readonly IMapper _mapper;
    private readonly ICrudRepository<Follower> _followerRepository;
    private readonly ICrudRepository<User> _userRepository;
    private readonly ICrudRepository<Person> _personRepository;

    public FollowerService(ICrudRepository<Follower> followerRepository, ICrudRepository<User> userRepository,
        ICrudRepository<Person> personRepository, IMapper mapper) : base(mapper)
    {
        _followerRepository = followerRepository;
        _userRepository = userRepository;
        _personRepository = personRepository;
        _mapper = mapper;
    }
    public Result AddFollower(long userId, long followedUserId)
    {
        var user = _userRepository.Get(userId);
        var followedUser = _userRepository.Get(followedUserId);

        if (user == null || followedUser == null)
        {
            return Result.Fail("User not found.");
        }

        if (IsAlreadyFollowing(userId, followedUserId))
        {
            return Result.Fail("User is already following.");
        }

        var follower = new Follower(userId, followedUserId);
        _followerRepository.Create(follower);
        return Result.Ok();
    }
    public Result RemoveFollower(long userId, long followedUserId)
    {
        var existingFollower = GetFollowerByUserAndFollowerId(userId, followedUserId);

        if(existingFollower == null)
        {
            return Result.Fail("Follower not found.");
        }

        _followerRepository.Delete(existingFollower.Id);
        return Result.Ok();
    }

    private Follower GetFollowerByUserAndFollowerId(long userId, long followedUserId)
    {
        return _followerRepository
            .GetPaged(1, int.MaxValue)
            .Results
            .FirstOrDefault(f => f.UserId == userId && f.FollowedUserId == followedUserId);
    }


    public Result<PagedResult<UserProfileDto>> GetPagedFollowersByUserId(long userId, int page, int pageSize)
    {
        try
        {
            _userRepository.Get(userId);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }

        var allFollowers = _followerRepository
            .GetPaged(1, int.MaxValue)  
            .Results
            .Where(f => f.FollowedUserId == userId)
            .Select(f => f.UserId)
            .ToList();

        var pagedFollowerIds = allFollowers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var followerProfiles = pagedFollowerIds
            .Select(id => GetPersonByUserId(id))
            .Where(person => person != null)
            .Select(person => _mapper.Map<UserProfileDto>(person))
            .ToList();

        var pagedResult = new PagedResult<UserProfileDto>(followerProfiles, allFollowers.Count);

        return pagedResult;
    }


    private Person GetPersonByUserId(long userId)
    {
        var pagedPersons = _personRepository.GetPaged(1, int.MaxValue);

        return pagedPersons.Results.FirstOrDefault(person => person.UserId == userId);
    }

    private bool IsAlreadyFollowing(long userId, long followerId)
    {
        return _followerRepository
            .GetPaged(1, int.MaxValue)
            .Results
            .Any(f => f.UserId == userId && f.FollowedUserId == followerId);
    }
}
