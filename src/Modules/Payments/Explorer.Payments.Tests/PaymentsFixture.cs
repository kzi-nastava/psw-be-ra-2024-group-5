using Explorer.BuildingBlocks.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Tests
{
    public class PaymentsFixture : BaseWebIntegrationTest<PaymentsTestFactory>, IDisposable
    {
        public PaymentsFixture() : base(new PaymentsTestFactory()){}

        public void Dispose()
        {
            Factory.Dispose();
        }
    }
}
