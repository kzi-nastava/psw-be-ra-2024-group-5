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
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogImageService : CrudService<BlogImageDTO, BlogImage>, IBlogImageService
    {
        private readonly IMapper _mapper;

        public BlogImageService(ICrudRepository<BlogImage> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
        }
        public Result<BlogImageDTO> CreateImage(BlogImageDTO imageDto)
        {
            if (imageDto == null || string.IsNullOrWhiteSpace(imageDto.base64Data))
            {
                return Result.Fail("Image data is required.");
            }

            var imageData = Convert.FromBase64String(imageDto.base64Data);

            var image = new BlogImage(imageData, imageDto.contentType);

            var imageDTO = _mapper.Map<BlogImageDTO>(image);

            var createResult = Create(imageDTO);

            if (createResult.IsSuccess)
            {
                return Result.Ok(createResult.Value);
            }
            else
            {
                return Result.Fail(createResult.Errors);
            }
        }
    }
}
