using Explorer.Encounters.Core.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure;

public static class EncountersStartup {

    public static IServiceCollection ConfigureEncountersModule(this IServiceCollection services) {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(EncountersProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services) {

    }

    private static void SetupInfrastructure(IServiceCollection services) {

    }
}
