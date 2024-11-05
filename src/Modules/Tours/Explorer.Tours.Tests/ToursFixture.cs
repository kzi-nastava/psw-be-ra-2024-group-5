using Explorer.BuildingBlocks.Tests;

namespace Explorer.Tours.Tests;

public class ToursFixture : BaseWebIntegrationTest<ToursTestFactory>, IDisposable
{
    public ToursFixture() : base(new ToursTestFactory()) { }

    public void Dispose()
    {
        Factory.Dispose();
    }
}