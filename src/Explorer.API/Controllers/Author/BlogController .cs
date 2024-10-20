using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/author/blog")]
    public class BlogController : BaseApiController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogDTO>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogService.getAll(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public  ActionResult<BlogDTO> CreateBlog([FromBody] BlogDTO blogDTO)
        {
            if (string.IsNullOrWhiteSpace(blogDTO.title))
                return BadRequest("Title is required.");
            else if (string.IsNullOrWhiteSpace(blogDTO.description))
                return BadRequest("Description is required.");

            var result = _blogService.CreateBlog(blogDTO);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}/status/{userId:int}")]
        public ActionResult<BlogDTO> UpdateStatus(int id, [FromBody] BlogStatusDto newStatus, int userId)
        {

            var result = _blogService.UpdateBlogStatus(id, newStatus, userId);
            return CreateResponse(result);
        }

        [HttpGet("preview/{blogId:int}")]
        public ActionResult<string> Preview(int blogId)
        {
            var blog = _blogService.getBlogById(blogId);
            var result = _blogService.PreviewBlogDescription(blog.Value.description);
            return Ok(result);
        }
    }
}
