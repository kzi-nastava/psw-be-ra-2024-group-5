using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly ICrudRepository<User> _userRepository;
    private readonly ICrudRepository<Person> _personRepository;

    public AccountService(IMapper mapper, ICrudRepository<User> userRepository, ICrudRepository<Person> personRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _personRepository = personRepository;
    }

    public Result Block(long userId)
    {
        var user = _userRepository.Get(userId);
        if (user == null) return Result.Fail(FailureCode.NotFound);

        user.IsActive = false;
        user = _userRepository.Update(user);

        return user != null ? Result.Ok() : Result.Fail(FailureCode.Internal);
    }

    public Result<PagedResult<AccountDto>> GetPaged(int page, int pageSize)
    {
        if (page < 1 || pageSize < 1) return Result.Fail(FailureCode.InvalidArgument);
        List<AccountDto> result = new List<AccountDto>();

        try
        {
            var persons = _personRepository.GetPaged(page, pageSize);
            foreach (var person in persons.Results)
            {
                var user = _userRepository.Get(person.UserId);
                if (user == null) continue;
                
                var account = _mapper.Map<AccountDto>(person);
                account = _mapper.Map(user, account);
                result.Add(account);
            }

            return new PagedResult<AccountDto>(result, persons.TotalCount);
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.Internal).WithError(e.Message);
        }
    }
}
