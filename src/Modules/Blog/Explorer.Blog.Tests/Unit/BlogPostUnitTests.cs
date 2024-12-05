using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Explorer.Blog.Core.Domain;


namespace Explorer.Blog.Tests.Unit
{
    public class BlogPostUnitTests
    {
        [Theory]
        [InlineData("Valid Title", "Valid Description", 1, true)] // Valid data
        [InlineData("", "Valid Description", 1, false)]           // Invalid title
        [InlineData("Valid Title", "", 1, false)]                 // Invalid description
        public void Constructor_Test(string title, string description, int userId, bool shouldSucceed)
        {
            // Act & Assert
            if (shouldSucceed)
            {
                var blogPost = new BlogPost(title, description, userId);
                Assert.Equal(title, blogPost.Title);
                Assert.Equal(description, blogPost.Description);
                Assert.Equal(userId, blogPost.UserId);
                Assert.Equal(BlogStatus.Draft, blogPost.Status);
            }
            else
            {
                Assert.ThrowsAny<ArgumentException>(() =>
                {
                    new BlogPost(title, description, userId);
                });
            }
        }

        [Theory]
        [InlineData("New Title", "New Description", 1, true)] // Valid update
        [InlineData("", "New Description", 1, false)]        // Invalid title
        [InlineData("New Title", "", 1, false)]              // Invalid description
        [InlineData("New Title", "New Description", 999, false)] // Invalid userId
        public void UpdateBlog_Test(string newTitle, string newDescription, int userId, bool shouldSucceed)
        {
            // Arrange
            var blogPost = CreateTestBlog();

            // Act & Assert
            if (shouldSucceed)
            {
                blogPost.UpdateBlog(newTitle, newDescription, userId);
                Assert.Equal(newTitle, blogPost.Title);
                Assert.Equal(newDescription, blogPost.Description);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    blogPost.UpdateBlog(newTitle, newDescription, userId);
                });
            }
        }

        [Theory]
        [InlineData(BlogStatus.Published, 1, true)] // Valid status update
        [InlineData(BlogStatus.Famous, 999, false)] // Invalid userId
        [InlineData((BlogStatus)999, 1, false)]     // Invalid status
        public void UpdateStatus_Test(BlogStatus newStatus, int userId, bool shouldSucceed)
        {
            // Arrange
            var blogPost = CreateTestBlog();

            // Act & Assert
            if (shouldSucceed)
            {
                blogPost.UpdateStatus(newStatus, userId);
                Assert.Equal(newStatus, blogPost.Status);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    blogPost.UpdateStatus(newStatus, userId);
                });
            }
        }

        [Theory]
        [InlineData(1, "Comment Text", 1, true)]   // Valid comment
        [InlineData(1, "Comment Text", 999, true)] // Valid comment by another user
        public void AddComment_Test(long id, string text, int userId, bool shouldSucceed)
        {
            // Arrange
            var blogPost = CreateTestBlog();

            // Act & Assert
            if (shouldSucceed)
            {
                blogPost.AddComment(id, text, userId);
                Assert.Contains(blogPost.Comments, c => c.CommentText == text && c.UserId == userId);
            }
        }

        [Theory]
        [InlineData(0, "Updated Comment", 1, true)]  // Valid edit
        [InlineData(0, "Updated Comment", 999, false)] // Invalid userId
        public void EditComment_Test(long commentId, string newCommentText, int userId, bool shouldSucceed)
        {
            // Arrange
            var blogPost = CreateTestBlog();
            blogPost.AddComment(blogPost.Id, "Original Comment", 1);

            // Act & Assert
            if (shouldSucceed)
            {
                blogPost.EditComment(commentId, newCommentText, userId);
                Assert.Contains(blogPost.Comments, c => c.Id == commentId && c.CommentText == newCommentText);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    blogPost.EditComment(commentId, newCommentText, userId);
                });
            }
        }

        [Theory]
        [InlineData(VoteType.Upvote, 1, true)]   // Valid vote
        [InlineData(VoteType.Upvote, 999, true)] // Valid vote by another user
        [InlineData(VoteType.Upvote, 1, false)]  // Invalid for draft status
        public void AddOrUpdateRating_Test(VoteType voteType, int userId, bool shouldSucceed)
        {
            // Arrange
            var blogPost = CreateTestBlog();
            if (shouldSucceed) blogPost.UpdateStatus(BlogStatus.Published, blogPost.UserId);

            // Act & Assert
            if (shouldSucceed)
            {
                blogPost.AddOrUpdateRating(voteType, userId);
                Assert.Contains(blogPost.Votes, v => v.UserId == userId && v.Type == voteType);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    blogPost.AddOrUpdateRating(voteType, userId);
                });
            }
        }

        [Theory]
        [InlineData(new byte[] { 1, 2, 3 }, "image/png", BlogStatus.Published, true)]  // Valid image
        [InlineData(new byte[] { 1, 2, 3 }, "image/png", BlogStatus.Closed, false)]   // Invalid for closed status
        public void AddImage_Test(byte[] imageData, string contentType, BlogStatus status, bool shouldSucceed)
        {
            // Arrange
            var blogPost = CreateTestBlog();
            blogPost.UpdateStatus(status, blogPost.UserId);

            // Act & Assert
            if (shouldSucceed)
            {
                blogPost.AddImage(imageData, contentType);
                Assert.Contains(blogPost.Images, img => img.Base64Data.SequenceEqual(imageData) && img.ContentType == contentType);
            }
            else
            {
                Assert.ThrowsAny<Exception>(() =>
                {
                    blogPost.AddImage(imageData, contentType);
                });
            }
        }

        private static BlogPost CreateTestBlog()
        {
            return new BlogPost("Test Title", "Test Description", 1);
        }
    }
}
