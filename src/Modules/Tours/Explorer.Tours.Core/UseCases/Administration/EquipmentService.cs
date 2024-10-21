using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class EquipmentService : CrudService<EquipmentDto, Equipment>, IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;

    public EquipmentService(IEquipmentRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _equipmentRepository = repository;
    }

    public Result<EquipmentDto> FindByName(string name)
    {
        var e = _equipmentRepository.FindByName(name);
        if (e == null)
            return Result.Fail<EquipmentDto>("Equipment not found");

        return MapToDto(e);
    }
}   