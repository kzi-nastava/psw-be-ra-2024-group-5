using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
    [Route("api/author/blog")]
    public class BlogPostController : BaseApiController
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpPost("create")]
        public ActionResult<CreateBlogPostDto> CreateBlog([FromBody] CreateBlogPostDto dto)
        {
            var result = _blogPostService.CreateBlogPost(dto.title, dto.description, dto.userId, dto.images);
            return CreateResponse(result);
        }


        [HttpGet("all")]
        public async Task<ActionResult<PagedResult<BlogPostDto>>> GetAllBlogPosts(int page, int pageSize)
        {
            var result = await _blogPostService.GetAllBlogPosts(page, pageSize);
            return CreateResponse(result);
        }


        [HttpGet("{id}")]
        public ActionResult<BlogPostDto> GetBlogPostById(long id)
        {
            var result = _blogPostService.GetBlogPostById(id);
            return CreateResponse(result);
        }

        [HttpPut("{id}")]
        public ActionResult<BlogPostDto> UpdateBlogPost(long id, [FromBody] CreateBlogPostDto dto)
        {
            var result = _blogPostService.UpdateBlogPost(id, dto.title, dto.description, dto.userId);
            return CreateResponse(result);

        }

        [HttpDelete("{id}")]
        public ActionResult<BlogPostDto> DeleteBlogPost(long id, [FromQuery] int userId)
        {
            var result = _blogPostService.DeleteBlogPost(id, userId);
            return CreateResponse(result);
        }

        [HttpPut("{blogId}/status")]
        public ActionResult<BlogPostDto> UpdateStatus(long blogId, [FromBody] BlogStatusDto newStatus, [FromQuery] int userId)
        {

            var result = _blogPostService.UpdateStatus(blogId, newStatus, userId);
            return CreateResponse(result);
        }


        [HttpPost("{blogId}/comments")]
        public ActionResult<BlogCommentDto> AddComment(long blogId, [FromBody] BlogCommentDto dto)
        {
            var result = _blogPostService.AddComment(blogId, dto.commentText, (int)dto.userId);
            return CreateResponse(result);
        }

        [HttpGet("{blogId}/comments")]
        public ActionResult<IReadOnlyCollection<BlogCommentDto>> GetAllComments(long blogId)
        {
            var result = _blogPostService.GetAllComments(blogId);
            return CreateResponse(result);
        }

        [HttpPut("{blogId}/comments/{commentId}")]
        public ActionResult<BlogCommentDto> EditComment(long blogId, long commentId, [FromBody] BlogCommentDto dto)
        {
            var result = _blogPostService.EditComment(blogId, commentId, dto.commentText, (int)dto.userId);
            return CreateResponse(result);
        }

        [HttpDelete("{blogId}/comments/{commentId}")]
        public ActionResult<BlogCommentDto> RemoveComment(long blogId, long commentId, [FromQuery] int userId)
        {
            var result = _blogPostService.RemoveComment(blogId, commentId, userId);
            return CreateResponse(result);
        }

        [HttpPost("{blogId}/images")]
        public ActionResult AddImage(long blogId, [FromBody] BlogImageDto dto)
        {
            var result = _blogPostService.AddImage(blogId, dto.base64Data, dto.contentType);
            return CreateResponse(result);
        }

        [HttpGet("{blogId}/images")]
        public ActionResult<IReadOnlyCollection<BlogImageDto>> GetAllImages(long blogId)
        {
            var result = _blogPostService.GetAllImages(blogId);
            return CreateResponse(result);
        }

        [HttpDelete("{blogId}/images")]
        public ActionResult RemoveImage(long blogId, [FromBody] BlogImageDto dto)
        {
            var result = _blogPostService.RemoveImage(blogId, dto.base64Data, dto.contentType);
            return CreateResponse(result);
        }

        [HttpPost("{blogId}/vote")]
        public ActionResult<BlogVoteDto> AddVote(long blogId, [FromBody] BlogVoteDto dto)
        {
            Console.WriteLine($"Received vote data: userId = {dto.userId}, type = {dto.type}, voteTime = {dto.voteTime}");

            var result = _blogPostService.AddVote(blogId, dto.type, dto.userId);
            return CreateResponse(result);
        }

        [HttpDelete("{blogId}/vote")]
        public ActionResult<BlogVoteDto> RemoveVote(long blogId, [FromQuery] int userId)
        {
            var result = _blogPostService.RemoveVote(blogId, userId);
            return CreateResponse(result);
        }

        [HttpGet("{blogId}/upvotes")]
        public ActionResult<int> GetUpvoteCount(long blogId)
        {
            var result = _blogPostService.GetUpvoteCount(blogId);
            return CreateResponse(result);
        }

        [HttpGet("{blogId}/downvotes")]
        public ActionResult<int> GetDownvoteCount(long blogId)
        {
            var result = _blogPostService.GetDownvoteCount(blogId);
            return CreateResponse(result);
        }

        [HttpGet("{blogId}/render")]
        public ActionResult<string> RenderDescriptionToMarkdown(long blogId)
        {
            var result = _blogPostService.RenderDescriptionToMarkdown(blogId);
            return CreateResponse(result);
        }

        [HttpPut("{blogId}/update-status")]
        public ActionResult<BlogPostDto> UpdateBlogStatusBasedOnVotesAndComments(long blogId, [FromQuery] int userId)
        {
            var result = _blogPostService.UpdateBlogStatusBasedOnVotesAndComments(blogId, userId);
            return CreateResponse(result);
        }


    }
}
