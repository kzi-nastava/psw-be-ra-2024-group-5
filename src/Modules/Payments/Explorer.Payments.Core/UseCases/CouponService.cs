using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Author;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
	public class CouponService : ICouponService
	{

		private readonly ICouponRepository _couponRepository;
		private readonly ITourRepository _tourRepository;

		public CouponService(ICouponRepository couponRepository, ITourRepository tourRepository) {
			_couponRepository = couponRepository;
			_tourRepository = tourRepository;

		}

		public Result<CouponDto> Create(CouponDto couponDto)
		{
			try
			{
				if (couponDto.Percentage < 1 || couponDto.Percentage > 100)
				{
					return Result.Fail<CouponDto>("Discount percentage must be between 1 and 100.");
				}

				var existingCoupon = _couponRepository.GetByCode(couponDto.Code);
				if (existingCoupon != null)
				{
					return Result.Fail<CouponDto>("A coupon with the given code already exists.");
				}

				var code = couponDto.Code ?? Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

				var coupon = new Coupon
				{
					Code = GenerateCouponCode(),
					Percentage = couponDto.Percentage,
					ExpiredDate = couponDto.ExpiredDate,
					TourIds = couponDto.TourIds,
				};

				_couponRepository.Add(coupon);
				_couponRepository.SaveChanges();

				// Mapiranje na DTO
				var createdCouponDto = new CouponDto
				{
					Code = coupon.Code,
					Percentage = coupon.Percentage,
					ExpiredDate = coupon.ExpiredDate,
					TourIds = coupon.TourIds,
				};

				return Result.Ok(createdCouponDto);
			}
			catch (Exception ex)
			{
				// Obrada grešaka
				return Result.Fail<CouponDto>($"An error occurred: {ex.Message}");
			}
		}
		public string GenerateCouponCode()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			return new string(Enumerable.Repeat(chars, 8)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public Result Delete(long id)
		{
			var coupon = _couponRepository.GetById(id);
			if (coupon == null)
			{
				return Result.Fail("Coupon not found.");
			}

			_couponRepository.Delete(coupon);
			_couponRepository.SaveChanges();

			return Result.Ok();
		}
		public Result<CouponDto> Update(long id, CouponDto couponDto)
		{
			var coupon = _couponRepository.GetById(id);
			if (coupon == null)
			{
				return Result.Fail<CouponDto>("Coupon not found.");
			}

			coupon.Percentage = couponDto.Percentage;
			coupon.ExpiredDate = couponDto.ExpiredDate;
			coupon.TourIds = couponDto.TourIds;

			_couponRepository.Update(coupon);
			_couponRepository.SaveChanges();

			var updatedCouponDto = new CouponDto
			{
				Id = coupon.Id,
				Code = coupon.Code,
				Percentage = coupon.Percentage,
				ExpiredDate = coupon.ExpiredDate,
				TourIds = coupon.TourIds
			};

			return Result.Ok(updatedCouponDto);
		}
		public Result<List<CouponDto>> GetAll()
		{
			var coupons = _couponRepository.GetAll();

			if (coupons == null || !coupons.Any())
			{
				return Result.Fail<List<CouponDto>>("No coupons found.");
			}

			var couponDtos = coupons.Select(coupon => new CouponDto
			{
				Id = coupon.Id,
				Code = coupon.Code,
				Percentage = coupon.Percentage,
				ExpiredDate = coupon.ExpiredDate,
				TourIds = coupon.TourIds,
			}).ToList();

			return Result.Ok(couponDtos); // Vraća listu DTO objekata
		}

		public async Task<List<TourDto>> GetToursByIds(List<long> tourIds)
		{
			var tours = await _tourRepository.GetToursByIds(tourIds);

			// Pretvaramo Tour objekte u TourDto (ako koristite DTOs za slanje podataka)
			return tours.Select(t => new TourDto
			{
				Id = t.Id,
				Name = t.Name,
				Description = t.Description
			}).ToList();
		}


	}

}
