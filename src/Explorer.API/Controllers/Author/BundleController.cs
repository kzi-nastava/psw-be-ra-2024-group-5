using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.BundleDto;
using Explorer.Payments.API.Enum;
using Explorer.Payments.API.Public.Tourist;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Route("api/author/bundle")]
    public class BundleController: BaseApiController
    {
        private readonly IBundleService _bundleService;

        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
        }

        [HttpPost("create")]
        public ActionResult<BundleDetailsDto> CreateBlog([FromBody] CreateBundleDto dto)
        {
            var result = _bundleService.CreateBundle(dto);
            return CreateResponse(result);
        }

        [HttpPut("update")]
        public ActionResult<BundleDetailsDto> UpdateBundle([FromBody] UpdateBundleDto dto)
        {
            var result = _bundleService.UpdateBundle(dto);
            return CreateResponse(result);
        }

        [HttpPatch("{bundleId}/status")]
        public ActionResult<BundleDetailsDto> ChangeStatus(long bundleId, [FromQuery] long authorId, [FromBody] BundleStatus newStatus)
        {
            var result = _bundleService.ChangeStatus(bundleId, authorId, newStatus);
            return CreateResponse(result);
        }


        [HttpPost("add/addOrReoveItem")]
        public ActionResult<BundleDetailsDto> AddOrRemoveBundleItem([FromBody] AddOrRemoveBundleItemDto item)
        {
            var result = _bundleService.AddOrRemoveBundleItem(item);
            return CreateResponse(result);
        }

        [HttpGet("details/{bundleId}/{authorId}")]
        public ActionResult<BundleDetailsDto> BundleDetails(long bundleId, long authorId)
        {
            var result = _bundleService.GetBundleById(bundleId, authorId);
            return CreateResponse(result);
        }

        [HttpGet("all")]
        public ActionResult<PagedResult<BundleSummaryDto>> GetAllBundles([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _bundleService.GetAllBundles(page, pageSize);
            return CreateResponse(result);
        }

        [HttpDelete("{bundleId}/delete")]
        public ActionResult<BundleDetailsDto> DeleteBundle(long bundleId, [FromQuery] long authorId)
        {
            var result = _bundleService.DeleteBundle(bundleId, authorId);
            return CreateResponse(result);
        }
    }
}
