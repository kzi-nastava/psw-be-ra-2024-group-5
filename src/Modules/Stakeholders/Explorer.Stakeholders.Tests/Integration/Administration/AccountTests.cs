using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Administration;

[Collection("Sequential")]
public class AccountTests : BaseStakeholdersIntegrationTest
{
    public AccountTests(StakeholdersTestFactory factory) : base(factory) { }

    [Fact]
    public void Successfully_gets_all_accounts()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var response = ((ObjectResult)controller.GetAll(0, 0).Result).Value as PagedResult<AccountDto>;

        // Assert
        response.Results.ShouldNotBeEmpty();
        response.Results.Count.ShouldBe(6);
    }

    [Fact]
    public void Successfully_blocks_account()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (StatusCodeResult)controller.Block(-21);
        var users = ((ObjectResult)controller.GetAll(0, 0).Result).Value as PagedResult<AccountDto>;
        var user = users.Results.FirstOrDefault(u => u.Id == -21);

        // Assert
        result.StatusCode.ShouldBe(200);
        user.ShouldNotBeNull();
        user.IsActive.ShouldBeFalse();
    }

    private static AccountController CreateController(IServiceScope scope)
    {
        return new AccountController(scope.ServiceProvider.GetRequiredService<IAccountService>());
    }
}