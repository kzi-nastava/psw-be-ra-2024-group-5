using Explorer.BuildingBlocks.Tests;

namespace Explorer.Stakeholders.Tests;

public class StakeholdersFixture : BaseWebIntegrationTest<StakeholdersTestFactory>, IDisposable
{
    public StakeholdersFixture() : base(new StakeholdersTestFactory()) { }

    public void Dispose()
    {
        Factory.Dispose();
    }
}