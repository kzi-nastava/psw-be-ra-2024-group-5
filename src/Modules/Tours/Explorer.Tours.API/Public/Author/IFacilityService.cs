﻿using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Author
{
    public interface IFacilityService
    {
        Result<FacilityDto> Create(FacilityDto facility);
        Result<List<FacilityDto>> GetPaged(int page, int pageSize);

        Result<FacilityDto> Get(long id);
        Result<FacilityDto> Update(FacilityDto facilityDto);
    }
}
