using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Tourist
{
    [Collection("Sequential")]
    public class ShoppingCartCommandTests: BaseToursIntegrationTest
    {
        public ShoppingCartCommandTests(ToursTestFactory factory) : base(factory) { }

        // Test takes tourists from explorer v1 not the test database
        /*
        [Fact]
        public void Creates()
        {
            // Arrange 
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            long touristId = 1;

            // Act
            var result = ((ObjectResult)controller.Create(touristId).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TotalPrice.Amount.ShouldBe(0);
            result.Items.Count.ShouldBe(0);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
        }
    */

        [Fact]
        public void AddItem()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var orderItem = new OrderItemDto(-22, -2, "Tura2", new MoneyDto(20.0, 0));

            // Act
            var result = ((ObjectResult)controller.AddItemToCart(orderItem,-21).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TotalPrice.Amount.ShouldNotBe(0);
            result.Items.Count.ShouldBe(2);

            // Assert - Database
            var storedEntity = dbContext.OrderItems.FirstOrDefault(i => i.TourName == orderItem.TourName);
            storedEntity.ShouldNotBeNull();
            storedEntity.Price.Amount.ShouldBe(20.0);
        }

        private static ShoppingCartController CreateController(IServiceScope scope)
        {
            return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
