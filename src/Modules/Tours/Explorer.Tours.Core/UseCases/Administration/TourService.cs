using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Enum;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourService : BaseService<TourDto, Tour>, ITourService 
{
    private readonly ITourRepository _repository;
    private readonly IMapper equipmentMapper;
    //private readonly IMapper _mapper;

    public TourService(ITourRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
        equipmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<Equipment, EquipmentDto>()).CreateMapper();
        //_mapper = mapper;
    }
    public Result<TourDto> GetById(int id) {
        var tour = _repository.GetById(id);
        var result =  MapToDto(tour);
        return result;
    }
    public Result<List<TourDto>> GetByAuthorId(int id) {
        var tours = _repository.GetByAuthorId(id);
        return MapToDto(tours);
    }
    public Result<TourDto> Create(TourDto tourDto) {
        //tour.Status = TourStatus.Draft;
        //tour.Price = new Money(0.0, Currency.Rsd);
        var tour = MapToDomain(tourDto);
        var result = _repository.Create(tour);
        return MapToDto(result);
    }

    public Result<TourDto> Update(TourDto tourDto) {
        var tour = MapToDomain(tourDto);
        var result = _repository.Update(tour);
        return MapToDto(result);
    }

    public Result Delete(int id) {
        try {
            _repository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result UpdateTourEquipment(long tourId, List<long> equipmentId)
    {
        var result = _repository.UpdateTourEquipment(tourId, equipmentId);
        return result;
    }

    public Result<PagedResult<EquipmentDto>> GetTourEquipment(long tourId) {
        var result = _repository.GetTourEquipment(tourId); 
        var dtos = equipmentMapper.Map<List<EquipmentDto>>(result.Value.Results);

        return new PagedResult<EquipmentDto>(dtos, dtos.Count);
    }
}

