using Explorer.API.Controllers.Author;
using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
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
	public class TouristEquipmentCommandTest : BaseToursIntegrationTest
	{
		public TouristEquipmentCommandTest(ToursTestFactory factory) : base(factory) { }

		[Fact] 
		public void Update_equipment_to_tourist()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

			List<int> equipmentIds = new List<int> { -1, -2, -3 };
			int touristId = -21;

			// Act
			var result = controller.UpdateTouristEquipment(touristId, equipmentIds);

			// Assert
			dbContext.TouristEquipment.Count().ShouldBe(3);
		}

		private static TouristController CreateController(IServiceScope scope)
		{
			return new TouristController(scope.ServiceProvider.GetRequiredService<ITouristService>())
			{
				ControllerContext = BuildContext("-1")
			};
		}

	}
}
