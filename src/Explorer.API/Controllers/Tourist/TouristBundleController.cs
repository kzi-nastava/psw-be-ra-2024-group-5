using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.BundleDto;
using Explorer.Payments.API.Public.Tourist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist;

[Authorize(Policy = "touristPolicy")]
[Route("api/tourist/bundle")]
public class TouristBundleController : BaseApiController {

    private readonly IBundleService _bundleService;

    public TouristBundleController(IBundleService bundleService) {
        _bundleService = bundleService;
    }

    [HttpGet("all")]
    public ActionResult<PagedResult<BundleSummaryDto>> GetAllBundles([FromQuery] int page, [FromQuery] int pageSize) {
        var result = _bundleService.GetAllBundles(page, pageSize);
        return CreateResponse(result);
    }
}
