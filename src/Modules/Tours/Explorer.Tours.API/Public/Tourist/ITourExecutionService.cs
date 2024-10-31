using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Tourist {
    public interface ITourExecutionService {
        public Result<TourExecutionDto> StartTourExecution(TourExecutionDto tourExecutionDto);
        public Result<KeyPointProgressDto> Progress(TourExecutionDto tourExecution);
    }
}
