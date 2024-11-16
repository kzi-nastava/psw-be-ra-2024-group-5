using Explorer.BuildingBlocks.Tests;

namespace Explorer.Encounters.Tests;

public class StakeholdersFixture : BaseWebIntegrationTest<EncountersTestFactory>, IDisposable
{
    public StakeholdersFixture() : base(new EncountersTestFactory()) { }

    public void Dispose() 
    {
        Factory.Dispose();
    }
}