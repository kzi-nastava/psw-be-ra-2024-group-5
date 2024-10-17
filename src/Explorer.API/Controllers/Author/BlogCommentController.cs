using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Authorization;
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
                // Uveri se da CreationTime i LastEditedTime koriste UTC
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
            commentDto.id = id; //  ID komentara koji treba ažurirati
            var result = _blogCommentService.UpdateComment( commentDto);
            return CreateResponse(result);
        }

       
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _blogCommentService.DeleteComment(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<BlogCommentDTO> GetCommentById(int id)
        {
            var result = _blogCommentService.GetCommentById(id);
            return CreateResponse(result);
        }

        //[HttpGet("user/{userId:int}")]
        //public ActionResult<IEnumerable<BlogCommentDTO>> GetCommentsByUserId(int userId)
        //{
        //    var result = _blogCommentService.GetCommentsByUserId(userId);
        //    return CreateResponse(result);
        //}
    }
}
