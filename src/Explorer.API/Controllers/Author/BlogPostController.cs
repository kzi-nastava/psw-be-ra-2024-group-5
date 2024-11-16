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
            var result = _blogPostService.CreateBlogPost(dto.Title, dto.Description, dto.UserId, dto.Images);
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
            var result = _blogPostService.UpdateBlogPost(id, dto.Title, dto.Description, dto.UserId);
            return CreateResponse(result);

        }

        [HttpDelete("{id}")]
        public ActionResult<BlogPostDto> DeleteBlogPost(long id, [FromQuery] int userId)
        {
            var result = _blogPostService.DeleteBlogPost(id, userId);
            return CreateResponse(result);
        }

        [HttpPut("{BlogId}/Status")]
        public ActionResult<BlogPostDto> UpdateStatus(long blogId, [FromBody] BlogStatusDto newStatus, [FromQuery] int userId)
        {

            var result = _blogPostService.UpdateStatus(blogId, newStatus, userId);
            return CreateResponse(result);
        }


        [HttpPost("{BlogId}/Comments")]
        public ActionResult<BlogCommentDto> AddComment(long blogId, [FromBody] BlogCommentDto dto)
        {
            var result = _blogPostService.AddComment(blogId, dto.CommentText, (int)dto.UserId);
            return CreateResponse(result);
        }

        [HttpGet("{BlogId}/Comments")]
        public ActionResult<IReadOnlyCollection<BlogCommentDto>> GetAllComments(long blogId)
        {
            var result = _blogPostService.GetAllComments(blogId);
            return CreateResponse(result);
        }

        [HttpPut("{BlogId}/Comments/{commentId}")]
        public ActionResult<BlogCommentDto> EditComment(long blogId, long commentId, [FromBody] BlogCommentDto dto)
        {
            var result = _blogPostService.EditComment(blogId, commentId, dto.CommentText, (int)dto.UserId);
            return CreateResponse(result);
        }

        [HttpDelete("{BlogId}/Comments/{commentId}")]
        public ActionResult<BlogCommentDto> RemoveComment(long blogId, long commentId, [FromQuery] int userId)
        {
            var result = _blogPostService.RemoveComment(blogId, commentId, userId);
            return CreateResponse(result);
        }

        [HttpPost("{BlogId}/Images")]
        public ActionResult AddImage(long blogId, [FromBody] BlogImageDto dto)
        {
            var result = _blogPostService.AddImage(blogId, dto.Base64Data, dto.ContentType);
            return CreateResponse(result);
        }

        [HttpGet("{BlogId}/Images")]
        public ActionResult<IReadOnlyCollection<BlogImageDto>> GetAllImages(long blogId)
        {
            var result = _blogPostService.GetAllImages(blogId);
            return CreateResponse(result);
        }

        [HttpDelete("{BlogId}/Images")]
        public ActionResult RemoveImage(long blogId, [FromBody] BlogImageDto dto)
        {
            var result = _blogPostService.RemoveImage(blogId, dto.Base64Data, dto.ContentType);
            return CreateResponse(result);
        }

        [HttpPost("{BlogId}/vote")]
        public ActionResult<BlogVoteDto> AddVote(long blogId, [FromBody] BlogVoteDto dto)
        {
            Console.WriteLine($"Received vote data: UserId = {dto.UserId}, Type = {dto.Type}, VoteTime = {dto.VoteTime}");

            var result = _blogPostService.AddVote(blogId, dto.Type, dto.UserId);
            return CreateResponse(result);
        }

        [HttpDelete("{BlogId}/vote")]
        public ActionResult<BlogVoteDto> RemoveVote(long blogId, [FromQuery] int userId)
        {
            var result = _blogPostService.RemoveVote(blogId, userId);
            return CreateResponse(result);
        }

        [HttpGet("{BlogId}/upvotes")]
        public ActionResult<int> GetUpvoteCount(long blogId)
        {
            var result = _blogPostService.GetUpvoteCount(blogId);
            return CreateResponse(result);
        }

        [HttpGet("{BlogId}/downvotes")]
        public ActionResult<int> GetDownvoteCount(long blogId)
        {
            var result = _blogPostService.GetDownvoteCount(blogId);
            return CreateResponse(result);
        }

        [HttpGet("{BlogId}/render")]
        public ActionResult<string> RenderDescriptionToMarkdown(long blogId)
        {
            var result = _blogPostService.RenderDescriptionToMarkdown(blogId);
            return CreateResponse(result);
        }

    }
}
