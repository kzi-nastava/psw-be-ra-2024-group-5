using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogCommentService : CrudService<BlogCommentDTO, BlogComment>, IBlogCommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public BlogCommentService(ICommentRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _commentRepository = repository;
            _mapper = mapper;
        }

        public Result<BlogCommentDTO> CreateComment(BlogCommentDTO commentDto)
        {
            if (commentDto == null)
                return Result.Fail("Comment data is required.");

            commentDto.creationTime = DateTime.UtcNow;

            var result = Create(commentDto);

            if (result.IsSuccess)
                return Result.Ok(result.Value);

            return Result.Fail(result.Errors);
        }


        public Result<BlogCommentDTO> GetCommentById(int id)
        {
            var result = Get(id);

            if (result.IsSuccess)
                return Result.Ok(result.Value);

            return Result.Fail(result.Errors);
        }

        public Result<BlogCommentDTO> UpdateComment(BlogCommentDTO updatedCommentDto)
        {
            BlogComment existingComment;

            try
            {
                existingComment = CrudRepository.Get(updatedCommentDto.id);
            }
            catch (KeyNotFoundException)
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


            var commentResult = CrudRepository.Get(commentId);
            if (commentResult == null)
            {
                return Result.Fail(new FluentResults.Error("Comment not found").WithMetadata("StatusCode", 404));
            }

            try
            {
                CrudRepository.Delete(commentId);
            }
            catch (Exception e)
            {
                return Result.Fail(new FluentResults.Error("Failed to delete comment").WithMetadata("ErrorMessage", e.Message));
            }

            return Result.Ok(true);
        }




    }
}
