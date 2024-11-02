using AutoMapper;
using Explorer.Tours.API.Dtos.TourExecution;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Mappers
{
    public class TourExecutionProfile : Profile {

        public TourExecutionProfile() {
            CreateMap<KeyPointProgress, KeyPointProgressDto>();
            CreateMap<TourExecution, TourExecutionDto>();
        }
    }
}
