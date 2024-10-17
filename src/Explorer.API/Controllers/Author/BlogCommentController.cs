using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    // [Authorize(Policy = "userPolicy")] // Postavi odgovarajuću politiku autentifikacije za korisnike
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

                if (commentDto.userId <= 0 || string.IsNullOrWhiteSpace(commentDto.commentText))
                {
                    return BadRequest("Invalid userId or commentText");
                }

                if (commentDto.creationTime.Kind != DateTimeKind.Utc)
                {
                    commentDto.creationTime = commentDto.creationTime.ToUniversalTime();
                }

                if (commentDto.lastEditedTime.HasValue && commentDto.lastEditedTime.Value.Kind != DateTimeKind.Utc)
                {
                    commentDto.lastEditedTime = commentDto.lastEditedTime.Value.ToUniversalTime();
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
        public ActionResult<BlogCommentDTO> Update(int id, [FromBody] BlogCommentDTO commentDto)
        {
            try
            {
                commentDto.id = id;
                var result = _blogCommentService.UpdateComment(commentDto);

                if (result.IsFailed)
                {
                    var statusCode = result.Errors.FirstOrDefault()?.Metadata["StatusCode"] as int? ?? 500;
                    return StatusCode(statusCode, result.Errors.FirstOrDefault()?.Message);
                }

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
        }




        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
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




    }
}
