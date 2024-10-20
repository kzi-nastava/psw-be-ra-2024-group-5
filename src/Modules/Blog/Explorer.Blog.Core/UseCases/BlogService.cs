using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Utilities;
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
        private readonly ICrudRepository<DomainBlog> _blogRepository;
        private readonly ICrudRepository<BlogImage> _blogImageRepository;
        public BlogService(ICrudRepository<DomainBlog> blogRepository, IMapper mapper, ICrudRepository<BlogImage> blogImageRepository) : base(blogRepository, mapper)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _blogImageRepository = blogImageRepository;
        }

        public Result<BlogDTO> CreateBlog(BlogDTO blogDTO)
        {
            var blog = new DomainBlog(blogDTO.title, blogDTO.description, blogDTO.userId);

            _blogRepository.Create(blog);  

            if (blogDTO.imageData != null && blogDTO.imageData.Any())
            {
                foreach (var imageDto in blogDTO.imageData)
                {
                    var imageBytes = Base64Converter.ConvertToByteArray(imageDto.base64Data);

                    var blogImage = new BlogImage(imageBytes, imageDto.contentType, (int) blog.Id);

                    _blogImageRepository.Create(blogImage);

                }
            }

            blogDTO.Id = (int) blog.Id;  

            return Result.Ok(blogDTO);
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
            var blogResult = CrudRepository.Get(blogId);
            if (blogResult == null)
                return Result.Fail("Blog not found.");

            if (!Enum.IsDefined(typeof(BlogStatusDto), newStatus))
                return Result.Fail("Invalid blog status.");

            var blog = blogResult;
            try
            {
                blog.UpdateStatus((BlogStatus)newStatus, userId); 
            }
            catch (UnauthorizedAccessException e)
            {
                return Result.Fail("Unauthorized to change the blog status.").WithError(e.Message);
            }

            try
            {
                CrudRepository.Update(blog);
            }
            catch (Exception e)
            {
                return Result.Fail("Failed to update blog status.").WithError(e.Message);
            }

            var updatedBlogDto = _mapper.Map<BlogDTO>(blog);
            return Result.Ok(updatedBlogDto);
        }

        public Result<List<BlogDTO>> getAll(int page, int pageSize)
        {
            try
            {
                var pageBlogs = CrudRepository.GetPaged(page, pageSize);

                var blogDtos = pageBlogs.Results.Select(blog =>
                {
                    var blogDto = _mapper.Map<BlogDTO>(blog);

                    blogDto.description = Markdown.ToHtml(blogDto.description);

                    var pagedImages = _blogImageRepository.GetPaged(page, pageSize);
                    var blogImages = pagedImages.Results.Where(img => img.blogId == blog.Id).ToList();

                    if (blogImages.Any())
                    {
                        var images = blogImages.Select(image => new BlogImageDTO
                        {
                            Id = (int)image.Id,
                            blogId = image.blogId,
                            base64Data = Base64Converter.ConvertFromByteArray(image.image),
                            contentType = image.contentType
                        }).ToList();

                        blogDto.imageData = images;
                    }
                    else
                    {
                        blogDto.imageData = new List<BlogImageDTO>();
                    }
                    
                    return blogDto;
                }).ToList();

                var pagedResult = new PagedResult<BlogDTO>(blogDtos, pageBlogs.TotalCount);

                return Result.Ok<List<BlogDTO>>(blogDtos);

            }
            catch (Exception e)
            {
                return Result.Fail($"An error ocurred while retriving blogs: {e.Message}");
            }
        }
    }
}
