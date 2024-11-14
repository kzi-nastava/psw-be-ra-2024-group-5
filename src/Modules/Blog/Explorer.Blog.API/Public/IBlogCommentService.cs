using Explorer.Blog.API.Dtos;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface IBlogCommentService
    {
        Result<BlogCommentDto> CreateComment(BlogCommentDto commentDto);
        Result<BlogCommentDto> GetCommentById(int id);
        Result<BlogCommentDto> UpdateComment(long id, BlogCommentDto commentDto);
        Result<bool> DeleteComment(long id);
        public Result<List<BlogCommentDto>> GetAllCommentsByUser(long userId);

    }
}
