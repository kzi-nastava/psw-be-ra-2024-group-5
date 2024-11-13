using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    //[Authorize(Policy = "userPolicy")]
    [Route("api/blog/Comments")]
    public class BlogCommentController : BaseApiController
    {
        private readonly IBlogCommentService _blogCommentService;

        public BlogCommentController(IBlogCommentService blogCommentService)
        {
            _blogCommentService = blogCommentService;
        }

        [HttpPost]
        public ActionResult<BlogCommentDto> Create([FromBody] BlogCommentDto commentDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(commentDto.CommentText))
                {
                    return BadRequest("Invalid UserId or CommentText");
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
        public ActionResult<BlogCommentDto> Update(long id, [FromBody] BlogCommentDto commentDto)
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

        [HttpGet("user/{UserId}")]
        public ActionResult<List<BlogCommentDto>> GetCommentsByUser(long userId)
        {
            var result = _blogCommentService.GetAllCommentsByUser(userId);

            if (result.IsFailed)
            {
                return StatusCode(500, result.Errors.FirstOrDefault()?.Message);
            }

            return Ok(result.Value);
        }
    }
}
