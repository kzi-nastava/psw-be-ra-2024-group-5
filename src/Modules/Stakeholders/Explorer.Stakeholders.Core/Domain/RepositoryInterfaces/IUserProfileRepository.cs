﻿using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserProfileRepository
{
    UserProfile? Get(long profileId);
    UserProfile? GetByUserId(long userId);
    PagedResult<UserProfile> GetPaged(int page, int pageSize);
    UserProfile Create(UserProfile userProfile);
    UserProfile Update(UserProfile userProfile);
    void Delete(long profileId);
    bool Exists(long profileId);
}
