using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.BundleDto;
using Explorer.Payments.API.Enum;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.Tourist
{
    public interface IBundleService
    {
        Result<BundleDetailsDto> CreateBundle(CreateBundleDto dto);
        Result<BundleDetailsDto> UpdateBundle(UpdateBundleDto dto);
        Result<BundleDetailsDto> AddOrRemoveBundleItem(AddOrRemoveBundleItemDto item);
        public Result<BundleDetailsDto> GetBundleById(long bundleId, long authorId);
        Result<PagedResult<BundleSummaryDto>> GetAllBundles(int page, int pageSize);
        Result<List<BundleDetailsDto>> GetAllDetailedBundles(int page, int pageSize);
        Result<BundleDetailsDto> DeleteBundle(long bundleId, long authorId);
        Result<BundleDetailsDto> ChangeStatus(long bundleId, long authorId, BundleStatus newStatus);

    }
}
