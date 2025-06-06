﻿using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IBundleRepository : ICrudRepository<Bundle>
    {
        Bundle Update(Bundle aggregateRoot);
        List<Bundle> GetBundlesPublished(int page, int pageSize);
    }
}
