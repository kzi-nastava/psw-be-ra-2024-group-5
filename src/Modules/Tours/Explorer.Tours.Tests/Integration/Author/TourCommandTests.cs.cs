using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Enum;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;


namespace Explorer.Tours.Tests.Integration.Author;

[Collection("Sequential")]
public class TourCommandTests : BaseToursIntegrationTest {
    public TourCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates() {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourDto {
            Name = "Nova Tura",
            Description = "Deskripcija nove ture",
            Tags = "Tag prvi, tagDrugi, TagTreci",
            Level = TourLevel.Intermediate,
            Status = TourStatus.Finished,
            Price = 420.69,
            AuthorId = 2
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);
        result.Description.ShouldBe(newEntity.Description);
        result.Level.ShouldBe(newEntity.Level);
        result.Status.ShouldBe(TourStatus.Draft);
        result.Price.ShouldBe(0.0);
        result.AuthorId.ShouldBe(newEntity.AuthorId);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
        storedEntity.Name.ShouldBe(result.Name);
        storedEntity.Description.ShouldBe(result.Description);
        storedEntity.Level.ShouldBe(result.Level);
        storedEntity.Status.ShouldBe(TourStatus.Draft);
        storedEntity.Price.ShouldBe(0.0);
        storedEntity.AuthorId.ShouldBe(result.AuthorId);
    }

    [Fact]
    public void Create_fails_invalid_data() {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourDto {
            Name = "NE treba da radi",
            Description = "Deskripcija nove ture",
            Tags = "Tag prvi, tagDrugi, TagTreci",
            Level = TourLevel.Intermediate,
            Status = TourStatus.Finished,
            Price = 420.69,
            AuthorId = -1
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    /* [Fact]
    public void Updates() {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new TourDto {
            Id = 3,
            Name = "Izmenjena Tura",
            Description = "IZMENE",
            Tags = "izmenjenTag",
            Level = TourLevel.Advanced,
            Status = TourStatus.Active,
            Price = 1000.0,
            AuthorId = 3

        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(updatedEntity.Name);
        result.Description.ShouldBe(updatedEntity.Description);
        result.Level.ShouldBe(updatedEntity.Level);
        result.Status.ShouldBe(updatedEntity.Status);
        result.Price.ShouldBe(updatedEntity.Price);
        result.AuthorId.ShouldBe(updatedEntity.AuthorId);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == "Izmenjena Tura");
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
        storedEntity.Name.ShouldBe(result.Name);
        storedEntity.Description.ShouldBe(result.Description);
        storedEntity.Level.ShouldBe(result.Level);
        storedEntity.Status.ShouldBe(TourStatus.Draft);
        storedEntity.Price.ShouldBe(0.0);
        storedEntity.AuthorId.ShouldBe(result.AuthorId);
        var oldEntity = dbContext.Equipment.FirstOrDefault(i => i.Name == "Tura1");
        oldEntity.ShouldBeNull();
    } */

    [Fact]
    public void Update_fails_invalid_id() {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourDto {
            Id = -1000,
            Name = "Izmenjena Tura!",
            Description = "IZMENE!",
            Tags = "izmenjenTag!",
            Level = TourLevel.Advanced,
            Status = TourStatus.Active,
            Price = 1000.0,
            AuthorId = 3
        };

        // Act
        var result = (ObjectResult)controller.Update(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    [Fact]
    public void Deletes() {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(2);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.Tours.FirstOrDefault(i => i.Id == 2);
        storedCourse.ShouldBeNull();
    }

    [Fact]
    public void Delete_fails_invalid_id() {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = (ObjectResult)controller.Delete(-1000);

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(404);
    }

    private static TourController CreateController(IServiceScope scope) {
        return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>()) {
            ControllerContext = BuildContext("-1")
        };
    }
}