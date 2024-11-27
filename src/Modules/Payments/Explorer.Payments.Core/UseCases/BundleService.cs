using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Dtos.BundleDto;
using Explorer.Payments.API.Enum;
using Explorer.Payments.API.Public.Tourist;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Internal;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class BundleService: IBundleService
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _internalUserService;
        private readonly IInternalTourService _internalTourService;

        public BundleService(IBundleRepository bundleRepository, IMapper mapper, IUserService internalUserService, IInternalTourService internalTourService)
        {
            _bundleRepository = bundleRepository;
            _mapper = mapper;
            _internalUserService = internalUserService;
            _internalTourService = internalTourService;
        }

        public Result<BundleDetailsDto> AddOrRemoveBundleItem(AddOrRemoveBundleItemDto item)
        {
            if (!_internalUserService.CheckAuthorExists(item.AuthorId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Author does not exist.");

            var bundle = new Bundle();
            try
            {
                bundle = _bundleRepository.Get(item.BundleId);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError($"Bundle doesn't exists: {ex.Message}");
            }
            try
            {
                bundle.AddBundleItemOrRemoveIfAlreadyExists(item.AuthorId, item.Id);

                _bundleRepository.Update(bundle);

                var resultDto = _mapper.Map<BundleDetailsDto>(bundle);

                return Result.Ok(resultDto);

            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError($"An unexpected error occurred: {ex.Message}");
            }


        }

        public Result<BundleDetailsDto> GetBundleById(long bundleId, long authorId)
        {
            if (!_internalUserService.CheckAuthorExists(authorId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Author does not exist.");

            var bundle = new Bundle();
            try
            {
                bundle = _bundleRepository.Get(bundleId);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError($"Bundle doesn't exists: {ex.Message}");
            }

            var resultDto = _mapper.Map<BundleDetailsDto>(bundle);

            return Result.Ok(resultDto);
        }

        public Result<BundleDetailsDto> CreateBundle(CreateBundleDto dto)
        {
            if (!_internalUserService.CheckAuthorExists(dto.AuthorId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Author does not exist.");

            try
            {
                var bundle = _mapper.Map<Bundle>(dto);

                _bundleRepository.Create(bundle);

                var resultDto = _mapper.Map<BundleDetailsDto>(bundle);

                return Result.Ok(resultDto);

            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError($"An unexpected error occurred: {ex.Message}");
            }
        }

        public Result<BundleDetailsDto> DeleteBundle(long bundleId, long authorId)
        {
            if (!_internalUserService.CheckAuthorExists(authorId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Author does not exist.");

            var bundle = new Bundle();
            try
            {
                bundle = _bundleRepository.Get(bundleId);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError($"Bundle doesn't exists: {ex.Message}");
            }

            try
            {
                _bundleRepository.Delete(bundleId);

                var resultDto = _mapper.Map<BundleDetailsDto>(bundle);

                return Result.Ok(resultDto);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError($"An unexpected error occurred: {ex.Message}");
            }
        }

        public Result<PagedResult<BundleSummaryDto>> GetAllBundles(int page, int pageSize)
        {
            var pageResult = _bundleRepository.GetPaged(page, pageSize);

            var resultDto = pageResult.Results.Select(bundle => _mapper.Map<BundleSummaryDto>(bundle)).ToList();

            var pagedResult = new PagedResult<BundleSummaryDto>(resultDto, pageResult.TotalCount);

            return Result.Ok(pagedResult);
        }

        public Result<BundleDetailsDto> UpdateBundle(UpdateBundleDto dto)
        {
            if (!_internalUserService.CheckAuthorExists(dto.AuthorId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Author does not exist.");

            var bundle = new Bundle();
            try
            {
                bundle = _bundleRepository.Get(dto.Id);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Bundle doesn't exists.");
            }
            
            
            try
            {
                if (!string.IsNullOrWhiteSpace(dto.Name))
                    bundle.EditName(dto.AuthorId, dto.Name);

                if (dto.Price != null)
                {
                    var newPrice = new Money(dto.Price.Amount, dto.Price.Currency);
                    bundle.EditPrice(dto.AuthorId, newPrice);
                }

                _bundleRepository.Update(bundle);

                var resultDto = _mapper.Map<BundleDetailsDto>(bundle);

                return Result.Ok(resultDto);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError($"An unexpected error occurred: {ex.Message}");
            }
        }

        public Result<BundleDetailsDto> ChangeStatus(long bundleId, long authorId, BundleStatus newStatus)
        {
            if (!_internalUserService.CheckAuthorExists(authorId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Author does not exist.");

            var bundle = new Bundle();
            try
            {
                bundle = _bundleRepository.Get(bundleId);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Bundle doesn't exists.");
            }

            var tours = new List<TourDto>();

            foreach (var tourId in bundle.BundleItems)
            {
                var tourResult = _internalTourService.GetById(tourId);

                if (tourResult != null && tourResult.IsSuccess)
                    tours.Add(tourResult.Value);
            }

            var publishedToursCount = tours.Count(tour => tour.Status == Tours.API.Enum.TourStatus.Published);

            try
            {
                if (newStatus != BundleStatus.Published)
                    bundle.ChangeStatus(authorId, newStatus);

                else if (publishedToursCount >= 2)
                    bundle.ChangeStatus(authorId, newStatus);
                else
                    return Result.Fail(FailureCode.InvalidArgument).WithError("At least 2 tours need to be published for you to be able to publishe bundle.");

                _bundleRepository.Update(bundle);

                var resultDto = _mapper.Map<BundleDetailsDto>(bundle);

                return Result.Ok(resultDto);

            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
