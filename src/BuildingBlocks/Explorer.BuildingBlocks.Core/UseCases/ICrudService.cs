using Explorer.BuildingBlocks.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.UseCases;
public interface ICrudService<TDto> {
    public Result<PagedResult<TDto>> GetPaged(int page, int pageSize);
    public Result<TDto> Get(int id);
    public Result<TDto> Create(TDto entity);
    public Result<TDto> Update(TDto entity);
    public Result Delete(int id);
}
