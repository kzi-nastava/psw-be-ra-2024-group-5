using Explorer.Tours.API.Dtos.TourLifecycle;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Internal
{
    public interface IInternalTourService
    {
        Result<TourDto> GetById(long id);
        Result<List<TourDto>>? GetByAuthorPaged(int authorId, int page, int pageSize);

    }
}
