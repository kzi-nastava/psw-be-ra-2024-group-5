using Explorer.Blog.API.Dtos;
using FluentResults;

namespace Explorer.Blog.API.Public
{
    public interface IBlogCommentService
    {
        Result<BlogCommentDTO> CreateComment(BlogCommentDTO commentDto);
        Result<BlogCommentDTO> GetCommentById(int id);
        Result<BlogCommentDTO> UpdateComment(long id, BlogCommentDTO commentDto);
        Result<bool> DeleteComment(long id);
        public Result<List<BlogCommentDTO>> GetAllCommentsByUser(long userId);

        public Result<List<BlogCommentDTO>> GetAllCommentsByBlogId(long blogId);
    }
}
