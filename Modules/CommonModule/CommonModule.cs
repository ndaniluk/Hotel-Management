﻿using Microsoft.Extensions.DependencyInjection;
using CommonModule.Factories;
using CommonModule.DataProviders;

namespace CommonModule
{
    public static class CommonModule
    {
        public static IServiceCollection AddCommonModules(this IServiceCollection servicesCollection)
        {
            return servicesCollection
                .AddCommonModuleFactories()
                .AddCommonModuleDataProviders();
        }
    }
}
