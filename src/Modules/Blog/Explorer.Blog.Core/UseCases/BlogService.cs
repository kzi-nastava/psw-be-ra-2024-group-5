using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
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
                return Result.Fail("Blog data is required.");

            var imageIds = new List<int>();
            if (blogDTO.imageData != null && blogDTO.imageData.Any())
            {
                foreach (var imageDto in blogDTO.imageData)
                {
                    var createImageResult = _blogImageService.CreateImage(imageDto);
                    if (createImageResult.IsSuccess)
                    {
                        imageIds.Add(createImageResult.Value.Id);
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

        public Result<BlogDTO> getBlogById(int id)
        {
            return Get(id);
        }

        public Result<BlogDTO> UpdateBlogStatus(int blogId, BlogStatusDto newStatus, int userId)
        {
            // Fetch the blog from the repository
            var blogResult = CrudRepository.Get(blogId);
            if (blogResult == null)
            {
                return Result.Fail("Blog not found.");
            }

            // Check if the new status is valid
            if (!Enum.IsDefined(typeof(BlogStatusDto), newStatus))
            {
                return Result.Fail("Invalid blog status.");
            }

            // Check if the user is authorized to update the status
            var blog = blogResult;
            try
            {
                blog.UpdateStatus((BlogStatus)newStatus, userId); // Assuming BlogStatusDto contains Status property of type BlogStatus
            }
            catch (UnauthorizedAccessException e)
            {
                return Result.Fail("Unauthorized to change the blog status.").WithError(e.Message);
            }

            // Update the blog in the repository
            try
            {
                CrudRepository.Update(blog);
            }
            catch (Exception e)
            {
                return Result.Fail("Failed to update blog status.").WithError(e.Message);
            }

            // Map the updated blog to a DTO and return success
            var updatedBlogDto = _mapper.Map<BlogDTO>(blog);
            return Result.Ok(updatedBlogDto);
        }

    }
}
