using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Blog.Core.Utilities;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using FluentResults;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogPostService : BaseService<BlogPostDto, BlogPost>, IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepositoy;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BlogPostService(IBlogPostRepository blogPostRepositoy, IMapper mapper, IUserService userService) : base(mapper)
        {
            _blogPostRepositoy = blogPostRepositoy;
            _mapper = mapper;
            _userService = userService;
        }


        public Result<CreateBlogPostDto> CreateBlogPost(string title, string description, int userId, List<BlogImageDto> images)
        {
            if (!_userService.CheckAuthorExists(userId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("Author does not exist.");

            BlogPost newBlogPost;
            try
            {
                newBlogPost = new BlogPost(title, description, userId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            foreach (var imageDto in images)
            {
                var imageData = Base64Converter.ConvertToByteArray(imageDto.Base64Data);

                newBlogPost.AddImage(imageData, imageDto.ContentType);
            }

            _blogPostRepositoy.Create(newBlogPost);
            var createdBlogDto = _mapper.Map<CreateBlogPostDto>(newBlogPost);
            return Result.Ok(createdBlogDto);
        }

        public Result<BlogPostDto> UpdateBlogPost(long id, string title, string description, int userId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(id);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");

            try
            {
                blogPost.UpdateBlog(title, description, userId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            _blogPostRepositoy.Update(blogPost);

            var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
            return Result.Ok(blogPostDto);
        }

        public Result<BlogPostDto> DeleteBlogPost(long id, int userId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(id);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");
            if (blogPost.UserId != userId) return Result.Fail(FailureCode.InvalidArgument).WithError("User is not authorized to delete this blog post.");
            _blogPostRepositoy.Delete(id);

            var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
            return Result.Ok(blogPostDto);
        }
        public Result<BlogPostDto> UpdateStatus(long blogId, BlogStatusDto newStatus, int userId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");

            try
            {
                blogPost.UpdateStatus((BlogStatus)newStatus, userId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            _blogPostRepositoy.Update(blogPost);

            var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
            return Result.Ok(blogPostDto);
        }

        public async Task<Result<PagedResult<BlogPostDto>>> GetAllBlogPosts(int page, int pageSize)
        {
            var pageResult = await _blogPostRepositoy.GetPagedBlogs(page, pageSize);

            var blogDtos = pageResult.Results.Select(blog => _mapper.Map<BlogPostDto>(blog)).ToList();
            var pagedResult = new PagedResult<BlogPostDto>(blogDtos, pageResult.TotalCount);

            return Result.Ok(pagedResult);
        }

        public Result<BlogPostDto> GetBlogPostById(long id)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.GetBlogPost((int)id);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");
            var blogPostDto = _mapper.Map<BlogPostDto>(blogPost);
            return Result.Ok(blogPostDto);
        }

        public Result<BlogCommentDto> AddComment(long blogId, string text, int userId)
        {
            if (!_userService.UserExists(userId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("User does not exist.");

            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");
            try
            {
                blogPost.AddComment(blogId, text, userId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            var commentToAdd = blogPost.Comments.FirstOrDefault(c => c.UserId == userId && c.BlogId == blogId);
            if (commentToAdd == null)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Comment not found.");
            }

            _blogPostRepositoy.Update(blogPost);

            var blogCommentDto = _mapper.Map<BlogCommentDto>(commentToAdd);
            return Result.Ok(blogCommentDto);
        }

        public Result<BlogCommentDto> EditComment(long blogId, long commentId, string newText, int userId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.GetBlogPost((int)blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");

            var commentToEdit = blogPost.Comments.FirstOrDefault(c => c.Id == commentId && c.UserId == userId);
            if (commentToEdit == null)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Comment not found.");
            }

            try
            {
                blogPost.EditComment(commentId, newText, userId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            _blogPostRepositoy.Update(blogPost);

            var blogCommentDto = _mapper.Map<BlogCommentDto>(commentToEdit);
            return Result.Ok(blogCommentDto);
        }

        public Result<BlogCommentDto> RemoveComment(long blogId, long commentId, int userId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.GetBlogPost((int)blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");

            var commentToRemove = blogPost.Comments.FirstOrDefault(c => c.Id == commentId && c.UserId == userId);
            if (commentToRemove == null)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Comment not found.");
            }
            try
            {
                blogPost.RemoveComment(commentId, userId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            _blogPostRepositoy.Update(blogPost);

            var blogCommentDto = _mapper.Map<BlogCommentDto>(commentToRemove);
            return Result.Ok(blogCommentDto);
        }

        public Result<IReadOnlyCollection<BlogCommentDto>> GetAllComments(long blogId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.GetBlogPost((int)blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");
            var comments = blogPost.GetAllComments();
            var commentsDto = _mapper.Map<List<BlogCommentDto>>(comments);
            return Result.Ok((IReadOnlyCollection<BlogCommentDto>)commentsDto);
        }

        public Result AddImage(long blogId, string imageData, string contentType)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");

            var image = Base64Converter.ConvertToByteArray(imageData);
            if (image == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid image data.");

            try
            {
                blogPost.AddImage(image, contentType);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result RemoveImage(long blogId, string imageData, string contentType)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");

            var image = Base64Converter.ConvertToByteArray(imageData);
            if (image == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Invalid image data.");


            try
            {
                blogPost.RemoveImage(image, contentType);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            _blogPostRepositoy.Update(blogPost);
            return Result.Ok();
        }

        public Result<IReadOnlyCollection<BlogImageDto>> GetAllImages(long blogId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");
            var images = blogPost.GetAllImages();
            var imagesDto = _mapper.Map<List<BlogImageDto>>(images);
            return Result.Ok((IReadOnlyCollection<BlogImageDto>)imagesDto);
        }

        public Result<BlogVoteDto> AddVote(long blogId, VoteTypeDto voteType, int userId)
        {
            if (!_userService.UserExists(userId))
                return Result.Fail(FailureCode.InvalidArgument).WithError("User does not exist.");

            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");

            try
            {
                blogPost.AddOrUpdateRating((VoteType)voteType, userId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            var voteToAdd = blogPost.Votes.FirstOrDefault(v => v.UserId == userId);
            if (voteToAdd == null)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Vote not found for the specified user.");
            }

            _blogPostRepositoy.Update(blogPost);

            var blogVoteDto = _mapper.Map<BlogVoteDto>(voteToAdd);
            return Result.Ok(blogVoteDto);
        }

        public Result<BlogVoteDto> RemoveVote(long blogId, int userId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");

            var voteToRemove = blogPost.Votes.FirstOrDefault(v => v.UserId == userId);
            if (voteToRemove == null)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError("Vote not found for the specified user.");
            }

            try
            {
                blogPost.RemoveRating(userId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

            _blogPostRepositoy.Update(blogPost);

            var blogVoteDto = _mapper.Map<BlogVoteDto>(voteToRemove);
            return Result.Ok(blogVoteDto);
        }

        public Result<int> GetUpvoteCount(long blogId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");
            return Result.Ok(blogPost.GetUpvoteCount());
        }

        public Result<int> GetDownvoteCount(long blogId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");
            return Result.Ok(blogPost.GetDownvoteCount());
        }

        public Result<string> RenderDescriptionToMarkdown(long blogId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            if (blogPost == null) return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");


            try
            {
                var renderedMarkdown = blogPost.RenderDescriptionToMarkdown();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            return Result.Ok();
        }

        public Result UpdateBlogStatusBasedOnVotesAndComments(long blogId, int userId)
        {
            BlogPost blogPost;
            try
            {
                blogPost = _blogPostRepositoy.Get(blogId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error fetching the blog post: " + e.Message);
                return Result.Fail(FailureCode.InvalidArgument).WithError("Error fetching the blog post: " + e.Message);
            }

            if (blogPost == null)
            {
                Console.WriteLine("Blog post not found.");
                return Result.Fail(FailureCode.InvalidArgument).WithError("Blog post not found.");
            }

            int upvotes = blogPost.GetUpvoteCount();
            int downvotes = blogPost.GetDownvoteCount();
            int commentCount = GetCommentCount(blogPost.Id);

            blogPost.UpdateStatusBasedOnVotesAndComments(upvotes, downvotes, commentCount, userId);

            try
            {
                _blogPostRepositoy.Update(blogPost);
                Console.WriteLine("Blog Status updated successfully in the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving the blog post Status to the database: " + ex.Message);
                return Result.Fail("Database error").WithError("Error saving the blog post Status: " + ex.Message);
            }

            return Result.Ok();
        }

        public int GetCommentCount(long blogId)
        {
            return _blogPostRepositoy.GetCommentCountForBlog(blogId);
        }


    }
}
