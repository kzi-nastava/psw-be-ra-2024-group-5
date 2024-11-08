using Explorer.API.Controllers;
using Explorer.API.Controllers.Identity;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Messages;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Stakeholders")]
    public class ProfileMessagesTests : IClassFixture<StakeholdersFixture>
    {
        private StakeholdersFixture fixture;

        public ProfileMessagesTests(StakeholdersFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Successfully_gets_profile_messages()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateProfileController(scope);
            var profileId = -12;

            // Act
            var response = ((ObjectResult)controller.GetProfile(profileId).Result).Value as UserProfileDto;

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(profileId);
            response.Messages.Count.ShouldBe(2);
        }

        [Fact]
        public void Successfully_send_messages()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateProfileController(scope);

            var message = new SendMessageDto
            {
                SenderId = -12,
                RecipientId = -11,
                Content = "Test Message",
                Attachment = new AttachmentDto(-5, (int)ResourceType.TourResource)
            };

            // Act
            var response = ((ObjectResult)CreateMessagesController(scope).SendMessage(message).Result).Value as MessageDto;
            var profile = ((ObjectResult)controller.GetProfile(-11).Result).Value as UserProfileDto;

            // Assert
            profile.ShouldNotBeNull();
            profile.Messages.Count.ShouldBe(1);

            response.ShouldNotBeNull();
            response.SenderId.ShouldBe(message.SenderId);
            response.Content.ShouldBe(message.Content);
            response.Attachment.ShouldNotBeNull();
        }

        [Fact]
        public void Fails_to_send_message_not_following()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var messageController = CreateMessagesController(scope);
            var profileController = CreateProfileController(scope);

            var message = new SendMessageDto
            {
                SenderId = -12,
                RecipientId = -13,
                Content = "Test Message Fail",
                Attachment = new AttachmentDto(-5, (int)ResourceType.TourResource)
            };

            // Act
            var response = (ObjectResult)messageController.SendMessage(message).Result;

            // Assert
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(403);
        }

        [Fact]
        public void Successfully_view_message()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var messageController = CreateMessagesController(scope);
            var profileController = CreateProfileController(scope);

            var viewMessageRequest = new ViewMessageDto
            {
                ProfileId = -12,
                MessageId = -4
            };

            // Act
            var result = (StatusCodeResult)messageController.ViewMessage(viewMessageRequest);
            var profile = ((ObjectResult)profileController.GetProfile(-12).Result).Value as UserProfileDto;
            var message = profile.Messages.Find(m => m.Id == viewMessageRequest.MessageId);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            message.IsRead.ShouldBeTrue();
        }

        private UserProfileController CreateProfileController(IServiceScope scope)
        {
            var controller = new UserProfileController(scope.ServiceProvider.GetRequiredService<IUserProfileService>());

            var claims = new List<Claim> { new Claim("id", "-12"), new Claim("id", "-11") };
            var identity = new ClaimsIdentity(claims, "Test");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            return controller;
        }

        private MessagesController CreateMessagesController(IServiceScope scope)
        {
            var controller = new MessagesController(scope.ServiceProvider.GetRequiredService<IProfileMessagesService>());

            var claims = new List<Claim> { new Claim("id", "-12"), new Claim("id", "-11") };
            var identity = new ClaimsIdentity(claims, "Test");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            return controller;
        }
    }
}
