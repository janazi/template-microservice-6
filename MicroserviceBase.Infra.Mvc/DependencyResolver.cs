using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceBase.Infra.Mvc;

public static class DependencyResolver
{
    internal static IServiceProvider? ServiceProvider { get; private set; }

    public static void Configure(IServiceProvider services)
    {
        if (ServiceProvider is not null)
            return;

        ServiceProvider = services;
    }

    public static T? GetService<T>() where T : class
    {
        if (ServiceProvider is null)
            throw new ArgumentException("Should call configure before try call");

        return ServiceProvider.GetService<T>();
    }
}
