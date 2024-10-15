using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        public ClubService(ICrudRepository<Club> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public Result<ClubDto> Create(ClubDto club)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result<ClubDto> Update(ClubDto club)
        {
            throw new NotImplementedException();
        }

        public Result<ClubDto> Get()
        {
            throw new NotImplementedException();
        }
    }
}
