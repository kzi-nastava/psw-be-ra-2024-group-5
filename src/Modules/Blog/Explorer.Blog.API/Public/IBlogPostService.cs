using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IBlogPostService
    {
        Result<CreateBlogPostDto> CreateBlogPost(string title, string description, int userId, List<BlogImageDto> images);
        Result<BlogPostDto> GetBlogPostById(long id);
        Task<Result<PagedResult<BlogPostDto>>> GetAllBlogPosts(int page, int pageSize);
        Result UpdateBlogPost(long id, string title, string description, int userId);
        Result DeleteBlogPost(long id, int userId);
        Result UpdateStatus(long blogId, BlogStatusDto newStatus, int userId);

        Result AddComment(long blogId, string text, int userId);
        Result EditComment(long blogId, long commentId, string newText, int userId);
        Result RemoveComment(long blogId, long commentId, int userId);
        Result<IReadOnlyCollection<BlogCommentDto>> GetAllComments(long blogId);

        Result AddVote(long blogId, VoteTypeDto voteType, int userId);
        Result RemoveVote(long blogId, int userId);
        Result<int> GetUpvoteCount(long blogId);
        Result<int> GetDownvoteCount(long blogId);

        Result AddImage(long blogId, string imageData, string contentType);
        Result RemoveImage(long blogId, string imageData, string contentType);
        Result<IReadOnlyCollection<BlogImageDto>> GetAllImages(long blogId);

        Result<string> RenderDescriptionToMarkdown(long blogId);
    }
}
