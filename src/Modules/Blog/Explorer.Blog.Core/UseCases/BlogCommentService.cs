using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogCommentService : CrudService<BlogCommentDTO, BlogComment>, IBlogCommentService
    {
        private readonly IMapper _mapper;

        public BlogCommentService(ICrudRepository<BlogComment> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
        }

        // Create a new comment
        public Result<BlogCommentDTO> CreateComment(BlogCommentDTO commentDto)
        {
            if (commentDto == null)
                return Result.Fail("Comment data is required.");

            // Call the base Create method from CrudService
            var result = Create(commentDto);

            if (result.IsSuccess)
                return Result.Ok(result.Value);

            return Result.Fail(result.Errors);
        }

        // Get a comment by ID
        public Result<BlogCommentDTO> GetCommentById(int id)
        {
            var result = Get(id);

            if (result.IsSuccess)
                return Result.Ok(result.Value);

            return Result.Fail(result.Errors);
        }

        // Update a comment
        public Result<BlogCommentDTO> UpdateComment(int commentId, BlogCommentDTO updatedCommentDto)
        {
            var commentResult = CrudRepository.Get(commentId);
            if (commentResult == null)
                return Result.Fail("Comment not found.");

            var existingComment = commentResult;

            // Map updated DTO fields to the existing comment
            _mapper.Map(updatedCommentDto, existingComment);

            try
            {
                CrudRepository.Update(existingComment);
            }
            catch (Exception e)
            {
                return Result.Fail("Failed to update comment.").WithError(e.Message);
            }

            var updatedCommentDtoMapped = _mapper.Map<BlogCommentDTO>(existingComment);
            return Result.Ok(updatedCommentDtoMapped);
        }

        // Delete a comment by ID
        public Result<bool> DeleteComment(long commentId)
        {
            var commentResult = CrudRepository.Get(commentId);
            if (commentResult == null)
                return Result.Fail("Comment not found.");

            try
            {
                // Umesto da šalješ ceo objekat, prosleđuješ samo ID
                CrudRepository.Delete(commentId);
            }
            catch (Exception e)
            {
                return Result.Fail("Failed to delete comment.").WithError(e.Message);
            }

            return Result.Ok(true);
        }



    }
}
