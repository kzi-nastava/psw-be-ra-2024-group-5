using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
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
        public BlogService(ICrudRepository<DomainBlog> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper; 
        }

        public BlogDTO CreateBlog(BlogDTO blogDTO)
        {
            var result = Create(blogDTO);

            if (result.IsSuccess)
                return result.Value;

            else
                throw new Exception(result.Errors.First().Message);

        }

        public string PreviewBlogDescription(string description)
        {
            return Markdown.ToHtml(description);
        }

        public BlogDTO UpdateBlogStatus(int blogId, BlogStatusDto newStatus, int userId)
        {
            var blog = Get(blogId);

            if (blog.IsFailed)
                throw new KeyNotFoundException("Blog not found.");

            var blogDto = blog.Value;

            if (blogDto.userId != userId)
                throw new UnauthorizedAccessException("Only blog creator can alter status.");

            if (!Enum.IsDefined(typeof(BlogStatusDto), newStatus))
                throw new ArgumentException("Invalid status.", nameof(newStatus));
            
            blogDto.status = newStatus;

            var updateResult = Update(blogDto);

            if (updateResult.IsSuccess)
                return updateResult.Value; 

            else
                throw new Exception(updateResult.Errors.First().Message);
        }
    }
}
