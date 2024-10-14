using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainBlog = Explorer.Blog.Core.Domain.Blog;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogDTO, DomainBlog>, IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IBlogImageService _blogImageService;
        public BlogService(ICrudRepository<DomainBlog> repository, IMapper mapper, IBlogImageService blogImageService) : base(repository, mapper)
        {
            _mapper = mapper;
            _blogImageService = blogImageService;
        }

        public Result<BlogDTO> CreateBlog(BlogDTO blogDTO)
        {
            if (blogDTO == null)
            {
                return Result.Fail("Blog data is required.");
            }

            var imageIds = new List<int>();
            if (blogDTO.imageData != null && blogDTO.imageData.Any())
            {
                foreach (var imageDto in blogDTO.imageData)
                {
                    var createImageResult = _blogImageService.CreateImage(imageDto);
                    if (createImageResult.IsSuccess)
                    {
                        imageIds.Add(createImageResult.Value.imageId);
                    }
                    else
                    {
                        return Result.Fail(createImageResult.Errors);
                    }
                }
            }

            blogDTO.imageIds = imageIds;

            var result = Create(blogDTO);

            if (result.IsSuccess)
                return Result.Ok(result.Value);

            else
                return Result.Fail(result.Errors);

        }

        public string PreviewBlogDescription(string description)
        {
            return Markdown.ToHtml(description);
        }

        public Result<BlogDTO> UpdateBlogStatus(int blogId, BlogStatusDto newStatus, int userId)
        {
            var blog = Get(blogId);

            if (blog.IsFailed)
                return Result.Fail(blog.Errors);

            var blogDto = blog.Value;

            if (blogDto.userId != userId)
                return Result.Fail("Only blog creator can alter status.");

            if (!Enum.IsDefined(typeof(BlogStatusDto), newStatus))
                return Result.Fail("Invalid status." + nameof(newStatus));
            
            blogDto.status = newStatus;

            var updateResult = Update(blogDto);

            if (updateResult.IsSuccess)
                return updateResult.Value; 

            else
                throw new Exception(updateResult.Errors.First().Message);
        }
    }
}
