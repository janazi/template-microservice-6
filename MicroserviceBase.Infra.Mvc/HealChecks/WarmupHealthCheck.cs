using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace MicroserviceBase.Infra.Mvc.HealChecks;

public class WarmupHealthCheck : IHealthCheck
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<WarmupHealthCheck> _logger;
    private static bool IsWarmedUp { get; set; }

    public WarmupHealthCheck(IServiceProvider serviceProvider, ILogger<WarmupHealthCheck> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (IsWarmedUp)
            return Task.FromResult(HealthCheckResult.Healthy());

        try
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()
                         .Where(a => a.GetName().Name!.Contains(AppDomain.CurrentDomain.FriendlyName)))
                assembly.GetTypes().Where(t => t.FullName!.Contains("Controller")).ToList()
                    .ForEach(t => _ = _serviceProvider.GetRequiredService(t));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        IsWarmedUp = true;

        return Task.FromResult(HealthCheckResult.Healthy());
    }
}
