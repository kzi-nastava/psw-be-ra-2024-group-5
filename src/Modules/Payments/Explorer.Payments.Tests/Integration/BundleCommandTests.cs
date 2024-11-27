using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos.BundleDto;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Tourist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.API.Enum;
using Shouldly;
using FluentResults;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Payments")]
    public class BundleCommandTests: IClassFixture<PaymentsFixture>
    {
        private PaymentsFixture fixture;

        public BundleCommandTests(PaymentsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("Test Bundle", 99.99, ShoppingCurrency.AC, -11, 200)]  // Success
        [InlineData("", 99.99, ShoppingCurrency.AC, -11, 400)]             // Missing Name
        [InlineData("Test Bundle", -1, ShoppingCurrency.AC, -11, 400)]     // Invalid Price
        [InlineData("Test Bundle", 99.99, ShoppingCurrency.AC, 999, 400)]   // Unauthorized user
        public async Task CreateBundle(string name, double price, ShoppingCurrency currency, long authorId, int expectedResponseCode)
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var createBundleDto = new CreateBundleDto
            {
                Name = name,
                Price = new ShoppingMoneyDto { Amount = price, Currency = currency },
                AuthorId = authorId,
                BundleItems = new List<long> { 101, 102 }
            };

            // Act
            var result = (ObjectResult)controller.CreateBlog(createBundleDto).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }

        [Theory]
        [InlineData(-1, "Updated Bundle", 150.00, -11, 200)] // Success
        [InlineData(-1, "", 150.00, -11, 200)]               // Missing Name shoud just update money
        [InlineData(-1, "Test", null, -11, 200)]
        [InlineData(-1, "Invalid price", -1.0, -11, 400)]     // Invalid Price
        [InlineData(999, "Bundle not found", 150.00, -11, 400)] // Bundle Not Found
        public async Task UpdateBundle(long bundleId, string name, object? price, long authorId, int expectedResponseCode)
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var updateBundleDto = new UpdateBundleDto
            {
                Id = bundleId,
                Name = name,
                Price = price is double priceValue
                    ? new ShoppingMoneyDto { Amount = priceValue, Currency = ShoppingCurrency.AC }
                    : null,
                AuthorId = authorId
            };

            // Act
            var result = (ObjectResult)controller.UpdateBundle(updateBundleDto).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }

        [Theory]
        [InlineData(-1, -11, 103, 200)]   // Add new item
        [InlineData(-4, -13, -1, 200)]   // Remove existing item
        [InlineData(-1, 999, 101, 400)]    // Invalid author
        [InlineData(-3, -12, -1, 400)]   // Less than two items in the bundle
        [InlineData(999, -11, 101, 400)] // Bundle not found
        public async Task AddOrRemoveBundleItem(long bundleId, long authorId, long itemId, int expectedResponseCode)
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var addOrRemoveItemDto = new AddOrRemoveBundleItemDto
            {
                BundleId = bundleId,
                AuthorId = authorId,
                Id = itemId
            };

            var result = (ObjectResult)controller.AddOrRemoveBundleItem(addOrRemoveItemDto).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }

        [Theory]
        [InlineData(-1, -11, BundleStatus.Published, 200)]   // Success with enough published tours
        [InlineData(-2, -11, BundleStatus.Draft, 200)]       // Success for non-published status
        [InlineData(-3, -12, BundleStatus.Archive, 200)]
        [InlineData(-1, 999, BundleStatus.Published, 400)]   // Invalid author
        [InlineData(-2, -11, BundleStatus.Published, 400)]   // Not enough published tours
        [InlineData(999, -11, BundleStatus.Published, 400)] // Bundle not found
        public async Task ChangeStatus(long bundleId, long authorId, BundleStatus newStatus, int expectedResponseCode)
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.ChangeStatus(bundleId, authorId, newStatus).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }

        [Theory]
        [InlineData(-1, -11, 200)]   // Success
        [InlineData(-1, 999, 400)]    // Invalid author
        [InlineData(999, -11, 400)] // Bundle not found
        public async Task DeleteBundle(long bundleId, long authorId, int expectedResponseCode)
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = (ObjectResult)controller.DeleteBundle(bundleId, authorId).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
        }

        private static BundleController CreateController(IServiceScope scope)
        {
            return new BundleController(scope.ServiceProvider.GetRequiredService<IBundleService>())
            {
                ControllerContext = PaymentsFixture.BuildContext("-1")
            };
        }
    }
}
