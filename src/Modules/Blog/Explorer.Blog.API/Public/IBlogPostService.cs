using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface IBlogPostService
    {
        Result<CreateBlogPostDto> CreateBlogPost(string title, string description, int userId, List<BlogImageDto> images);
        Result<BlogPostDto> GetBlogPostById(long id);
        Task<Result<PagedResult<BlogPostDto>>> GetAllBlogPosts(int page, int pageSize);
        Result<BlogPostDto> UpdateBlogPost(long id, string title, string description, int userId);
        Result<BlogPostDto> DeleteBlogPost(long id, int userId);
        Result<BlogPostDto> UpdateStatus(long blogId, BlogStatusDto newStatus, int userId);

        Result<BlogCommentDto> AddComment(long blogId, string text, int userId);
        Result<BlogCommentDto> EditComment(long blogId, long commentId, string newText, int userId);
        Result<BlogCommentDto> RemoveComment(long blogId, long commentId, int userId);
        Result<IReadOnlyCollection<BlogCommentDto>> GetAllComments(long blogId);

        Result<BlogVoteDto> AddVote(long blogId, VoteTypeDto voteType, int userId);
        Result<BlogVoteDto> RemoveVote(long blogId, int userId);
        Result<int> GetUpvoteCount(long blogId);
        Result<int> GetDownvoteCount(long blogId);

        Result AddImage(long blogId, string imageData, string contentType);
        Result RemoveImage(long blogId, string imageData, string contentType);
        Result<IReadOnlyCollection<BlogImageDto>> GetAllImages(long blogId);

        Result<string> RenderDescriptionToMarkdown(long blogId);

    }
}
