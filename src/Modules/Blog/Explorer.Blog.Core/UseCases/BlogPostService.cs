using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Blog.Core.Utilities;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogPostService : BaseService<BlogPostDto, BlogPost>, IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepositoy;
        private readonly IMapper _mapper;

        public BlogPostService(IBlogPostRepository blogPostRepositoy, IMapper mapper) : base(mapper)
        {
            _blogPostRepositoy = blogPostRepositoy;
            _mapper = mapper;
        }


        public Result<CreateBlogPostDto> CreateBlogPost(string title, string description, int userId, List<BlogImageDto> images)
        {
            var newBlogPost = new BlogPost(title, description, userId);

            foreach (var imageDto in images)
            {
                var imageData = Base64Converter.ConvertToByteArray(imageDto.base64Data);

                newBlogPost.AddImage(imageData, imageDto.contentType);
            }

            _blogPostRepositoy.Create(newBlogPost);
            var createdBlogDto = _mapper.Map<CreateBlogPostDto>(newBlogPost);
            return Result.Ok(createdBlogDto);
        }

        public Result UpdateBlogPost(long id, string title, string description, int userId)
        {
            var blogPost = _blogPostRepositoy.Get(id);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            blogPost.UpdateBlog(title, description, userId);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result DeleteBlogPost(long id, int userId)
        {
            var blogPost = _blogPostRepositoy.Get(id);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            if (blogPost.userId != userId) return Result.Fail("User is not authorized to delete this blog post.");
            _blogPostRepositoy.Delete(id);
            return Result.Ok();
        }

        public async Task<Result<PagedResult<BlogPostDto>>> GetAllBlogPosts(int page, int pageSize)
        {
            var pageResult = await _blogPostRepositoy.GetPagedBlogs(page, pageSize);

            var blogDtos = pageResult.Results.Select(blog => _mapper.Map<BlogPostDto>(blog)).ToList();
            var pagedResult = new PagedResult<BlogPostDto>(blogDtos, pageResult.TotalCount);

            return Result.Ok(pagedResult);
        }

        public Result<BlogPostDto> GetBlogPostById(long id)
        {
            var blogPost = _blogPostRepositoy.GetBlogPost((int)id);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
            return Result.Ok(blogPostDto);
        }

        public Result AddComment(long blogId, string text, int userId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            blogPost.AddComment(blogId, text, userId);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result EditComment(long blogId, long commentId, string newText, int userId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            blogPost.EditComment(commentId, newText, userId);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result RemoveComment(long blogId, long commentId, int userId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            blogPost.RemoveComment(commentId, userId);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result<IReadOnlyCollection<BlogCommentDto>> GetAllComments(long blogId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            var comments = blogPost.GetAllComments();
            var commentsDto = _mapper.Map<List<BlogCommentDto>>(comments);
            return Result.Ok((IReadOnlyCollection<BlogCommentDto>)commentsDto);
        }

        public Result AddImage(long blogId, string imageData, string contentType)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");

            var image = Base64Converter.ConvertToByteArray(imageData);
            if (image == null) return Result.Fail("Invalid image data.");

            blogPost.AddImage(image, contentType);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result RemoveImage(long blogId, string imageData, string contentType)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");

            var image = Base64Converter.ConvertToByteArray(imageData);
            if (image == null) return Result.Fail("Invalid image data.");

            blogPost.RemoveImage(image, contentType);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result<IReadOnlyCollection<BlogImageDto>> GetAllImages(long blogId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            var images = blogPost.GetAllImages();
            var imagesDto = _mapper.Map<List<BlogImageDto>>(images);
            return Result.Ok((IReadOnlyCollection<BlogImageDto>)imagesDto);
        }

        public Result AddVote(long blogId, VoteTypeDto voteType, int userId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            blogPost.AddOrUpdateRating((VoteType)voteType, userId);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result RemoveVote(long blogId, int userId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            blogPost.RemoveRating(userId);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result<int> GetUpvoteCount(long blogId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            return Result.Ok(blogPost.GetUpvoteCount());
        }

        public Result<int> GetDownvoteCount(long blogId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            return Result.Ok(blogPost.GetDownvoteCount());
        }

        public Result<string> RenderDescriptionToMarkdown(long blogId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            var renderedMarkdown = blogPost.RenderDescriptionToMarkdown();
            return Result.Ok(renderedMarkdown);
        }

        public Result UpdateStatus(long blogId, BlogStatusDto newStatus, int userId)
        {
            var blogPost = _blogPostRepositoy.Get(blogId);
            if (blogPost == null) return Result.Fail("Blog post not found.");
            blogPost.UpdateStatus((BlogStatus)newStatus, userId);
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }


    }
}
