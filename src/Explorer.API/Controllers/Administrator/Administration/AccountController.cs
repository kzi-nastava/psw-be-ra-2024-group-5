using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/accounts")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<PagedResult<AccountDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _accountService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("block/{id:int}")]
        public ActionResult Block(long id)
        {
            var result = _accountService.Block(id);
            return CreateResponse(result);
        }

    }
}
