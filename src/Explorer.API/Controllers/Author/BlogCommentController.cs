using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    // [Authorize(Policy = "userPolicy")]
    [Route("api/blog/comments")]
    public class BlogCommentController : BaseApiController
    {
        private readonly IBlogCommentService _blogCommentService;

        public BlogCommentController(IBlogCommentService blogCommentService)
        {
            _blogCommentService = blogCommentService;
        }

        [HttpPost]
        public ActionResult<BlogCommentDTO> Create([FromBody] BlogCommentDTO commentDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(commentDto.commentText))
                {
                    return BadRequest("Invalid userId or commentText");
                }

                var result = _blogCommentService.CreateComment(commentDto);

                return CreateResponse(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
        }



        [HttpPut("{id:int}")]
        public ActionResult<BlogCommentDTO> Update(long id, [FromBody] BlogCommentDTO commentDto)
        {
            try
            {

                var result = _blogCommentService.UpdateComment(id, commentDto);

                if (result.IsFailed && result.Errors.Any(e => e.Metadata.ContainsKey("StatusCode") && (int)e.Metadata["StatusCode"] == 404))
                {
                    return NotFound(result.Errors.First().Message);
                }

                if (result.IsFailed)
                {
                    return StatusCode(500, "Error during deletion: " + result.Errors.First().Message);
                }

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(long id)
        {
            var result = _blogCommentService.DeleteComment(id);

            if (result.IsFailed && result.Errors.Any(e => e.Metadata.ContainsKey("StatusCode") && (int)e.Metadata["StatusCode"] == 404))
            {
                return NotFound(result.Errors.First().Message);
            }

            if (result.IsFailed)
            {
                return StatusCode(500, "Error during deletion: " + result.Errors.First().Message);
            }

            return Ok(result.Value);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<BlogCommentDTO>> GetCommentsByUser(long userId)
        {
            var result = _blogCommentService.GetAllCommentsByUser(userId);

            if (result.IsFailed)
            {
                return StatusCode(500, result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }

        [HttpGet("blog/{blogId}")]
        public IActionResult GetCommentsForBlog(long blogId)
        {
            var result = _blogCommentService.GetAllCommentsByBlogId(blogId);

            if (result.IsFailed)
            {
                // Check if the error is specifically about not finding comments
                if (result.Errors.Any(e => e.Message == "No comments found for this blog."))
                {
                    return Ok(new List<BlogComment>()); // Return an empty list instead of 404
                }
                return NotFound(new { Message = result.Errors.First().Message });
            }

            return Ok(result.Value);
        }

    }
}
