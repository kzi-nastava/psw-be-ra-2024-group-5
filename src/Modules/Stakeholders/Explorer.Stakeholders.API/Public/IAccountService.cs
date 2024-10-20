using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IAccountService
{
    Result<PagedResult<AccountDto>> GetPaged(int page, int pageSize);
    Result Block(long userId);
}
