using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Messages;
using Explorer.Stakeholders.Core.Domain.Messages;

namespace Explorer.Encounters.Core.UseCases;

public class EncounterService : CrudService<EncounterDto, Encounter>, IEncounterService {
     
    private readonly IEncounterRepository _encounterRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;

    public EncounterService(IEncounterRepository encounterRepository, IMapper mapper, IUserRepository userRepository,
        INotificationService notificationService) 
        : base(encounterRepository, mapper) {

        _encounterRepository = encounterRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _notificationService = notificationService;

    }

    public Result<List<EncounterDto>> GetAllActive() {
        var encounters = _encounterRepository.GetAllActive();
        var encounterDtos = _mapper.Map<List<EncounterDto>>(encounters);
        return Result.Ok(encounterDtos);
    }

    public Result<List<EncounterDto>> GetByCreatorId(long creatorId) {
        var encounters = _encounterRepository.GetByCreatorId(creatorId);
        var encounterDtos = _mapper.Map<List<EncounterDto>>(encounters);
        return Result.Ok(encounterDtos);
    }

    public override Result<EncounterDto> Create(EncounterDto encounter)
    {
        var user = _userRepository.Get(encounter.CreatorId);

        try
        {
            var encounterToCreate = _encounterRepository.Create(MapToDomain(encounter));

            if (user != null && user.Role == UserRole.Tourist)
            {
                sendNotification();
            }

            return MapToDto(encounterToCreate);
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private void sendNotification()
    {
        var adminIds = _userRepository.GetAll()
                                  .Where(u => u.Role == UserRole.Administrator)
                                  .Select(u => u.Id)
                                  .ToList();

        var notificationDto = new NotificationDto
        {
            Content = "A new encounter has been created! " +
            "You can review the details and decide whether to approve or reject it.",
            CreatedAt = DateTime.UtcNow,
            UserIds = adminIds,
            Type = 3,
            UserReadStatuses = adminIds.Select(adminId => new NotificationReadStatusDto
            {
                UserId = adminId,
                NotificationId = 0,
                IsRead = false
            }).ToList()
        };

        _notificationService.SendNotification(notificationDto);
    }
}
