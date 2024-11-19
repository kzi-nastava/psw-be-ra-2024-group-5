using Explorer.API.Controllers.Tourist;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public.Tourist;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Payments")]
    public class ShoppingCartCommandTests : IClassFixture<PaymentsFixture>
    {
        private PaymentsFixture fixture;

        public ShoppingCartCommandTests(PaymentsFixture fixture)
        {
            this.fixture = fixture;
        }

        // Test takes tourists from explorer v1 not the test database

        //[Fact]
        //public void AddItem()
        //{
        //    // Arrange
        //    using var scope = fixture.Factory.Services.CreateScope();
        //    var controller = CreateController(scope);
        //    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
        //    var orderItem = new OrderItemDto(-22, -2, "Tura2", new ShoppingMoneyDto(20.0, 0));

        //    // Act
        //    var result = ((ObjectResult)controller.AddItemToCart(orderItem, -21).Result)?.Value as ShoppingCartDto;

        //    // Assert - Response
        //    result.ShouldNotBeNull();
        //    result.Id.ShouldNotBe(0);
        //    result.TotalPrice.Amount.ShouldNotBe(0);
        //    result.Items.Count.ShouldBe(2);

        //    // Assert - Database
        //    var storedEntity = dbContext.OrderItems.FirstOrDefault(i => i.TourName == orderItem.TourName);
        //    storedEntity.ShouldNotBeNull();
        //    storedEntity.Price.Amount.ShouldBe(20.0);
        //}

        private static ShoppingCartController CreateController(IServiceScope scope)
        {
            return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = PaymentsFixture.BuildContext("-1")
            };
        }
    }
}
