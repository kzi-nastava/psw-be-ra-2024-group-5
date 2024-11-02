using Explorer.Tours.API.Dtos.TourExecution;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Tourist
{
    public interface ITourExecutionService {

        public Result<TourExecutionDto> GetActive(long userId);
        public Result<TourExecutionDto> Start(TourExecutionStartDto tourExecutionStart);
        public Result<KeyPointProgressDto> Progress(long tourExecutionId, PositionDto newPositionDto);
    }
}
