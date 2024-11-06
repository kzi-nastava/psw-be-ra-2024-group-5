using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogCommentService : CrudService<BlogCommentDTO, BlogComment>, IBlogCommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public BlogCommentService(ICommentRepository repository, IUserRepository userRepository, IMapper mapper) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _commentRepository = repository;
            _mapper = mapper;
        }

        public Result<BlogCommentDTO> CreateComment(BlogCommentDTO commentDto)
        {
            if (commentDto == null)
                return Result.Fail("Comment data is required.");

            if (!_userRepository.UserExistsById(commentDto.userId))
            {
                return Result.Fail(new FluentResults.Error($"User with ID {commentDto.userId} does not exist."));
            }

            var blogComment = new BlogComment(commentDto.blogPostId, commentDto.userId, commentDto.commentText);

            Result<BlogComment> result = CrudRepository.Create(blogComment);

            if (result.IsSuccess)
            {
                var createdCommentDto = _mapper.Map<BlogCommentDTO>(blogComment);
                return Result.Ok(createdCommentDto);
            }

            return Result.Fail(result.Errors);
        }

        public Result<BlogCommentDTO> GetCommentById(int id)
        {
            var result = Get(id);

            if (result.IsSuccess)
                return Result.Ok(result.Value);

            return Result.Fail(result.Errors);
        }

        public Result<BlogCommentDTO> UpdateComment(long id, BlogCommentDTO updatedCommentDto)
        {
            var existingComment = CrudRepository.Get(id);
            if (existingComment == null)
            {
                return Result.Fail(new FluentResults.Error("Comment not found").WithMetadata("StatusCode", 404));
            }

            _mapper.Map(updatedCommentDto, existingComment);

            try
            {
                CrudRepository.Update(existingComment);
            }
            catch (Exception e)
            {
                return Result.Fail(new FluentResults.Error("Failed to update comment").WithMetadata("ErrorMessage", e.Message));
            }

            var updatedCommentDtoMapped = _mapper.Map<BlogCommentDTO>(existingComment);
            return Result.Ok(updatedCommentDtoMapped);
        }



        public Result<bool> DeleteComment(long commentId)
        {
            try
            {
                var commentResult = CrudRepository.Get(commentId);

                if (commentResult == null)
                {
                    return Result.Fail(new FluentResults.Error("Comment not found").WithMetadata("StatusCode", 404));
                }

                CrudRepository.Delete(commentId);
                return Result.Ok(true);
            }
            catch (KeyNotFoundException)
            {
                return Result.Fail(new FluentResults.Error("Comment not found").WithMetadata("StatusCode", 404));
            }
            catch (Exception e)
            {
                return Result.Fail(new FluentResults.Error("Failed to delete comment").WithMetadata("ErrorMessage", e.Message));
            }
        }


        public Result<List<BlogCommentDTO>> GetAllCommentsByUser(long userId)
        {
            try
            {
                if (!_userRepository.UserExistsById(userId))
                {
                    return Result.Fail(new FluentResults.Error($"User with ID {userId} does not exist."));
                }

                var comments = _commentRepository.GetAllByUser(userId);

                if (comments == null || comments.Count == 0)
                {
                    return Result.Fail(new FluentResults.Error("No comments found for this user."));
                }

                var commentDtos = _mapper.Map<List<BlogCommentDTO>>(comments);

                return Result.Ok(commentDtos);
            }
            catch (Exception ex)
            {
                return Result.Fail(new FluentResults.Error($"Error fetching comments for user {userId}: {ex.Message}"));
            }
        }

        public Result<List<BlogCommentDTO>> GetAllCommentsByBlogId(long blogId)
        {
            try
            {
                var comments = _commentRepository.GetAllByBlogId(blogId);

                if (comments == null || comments.Count == 0)
                {
                    return Result.Fail(new FluentResults.Error("No comments found for this blog."));
                }

                var commentDtos = _mapper.Map<List<BlogCommentDTO>>(comments);
                return Result.Ok(commentDtos);
            }
            catch (Exception ex)
            {
                return Result.Fail(new FluentResults.Error($"Error fetching comments for blog {blogId}: {ex.Message}"));
            }
        }


    }
}
