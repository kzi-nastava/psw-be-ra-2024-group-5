using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    
    [Authorize(Policy = "touristPolicy")]
    [Route("api/wallet")]
    public class WalletController: BaseApiController
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [AllowAnonymous]
        [Authorize(Policy = "administratorPolicy")]
        [HttpGet("admin/{touristId:long}")]
        public ActionResult<WalletDto> GetWalletByTouristIdForAdmin(long touristId)
        {
            var result = this._walletService.GetByTouristId(touristId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public ActionResult<WalletDto> GetWalletForTourist()
        {
            var userId = User.FindFirst("id")?.Value;
            if (userId == null || !long.TryParse(userId, out long parsedTouristId))
            {
                return BadRequest(Result.Fail("User ID not found or invalid"));
            }

            var result = this._walletService.GetByTouristId(parsedTouristId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }
            
            return Ok(result.Value);
        }

        [AllowAnonymous]
        [Authorize(Policy = "administratorPolicy")]
        [HttpPost("addFunds/{touristId:long}")]
        public ActionResult<WalletDto> AddFundsToTourist(ShoppingMoneyDto moneyDto, long touristId)
        {
            var result = this._walletService.AddFunds(moneyDto, touristId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }
    }
}
