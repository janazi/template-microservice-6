using Microsoft.Extensions.Logging;

namespace MicroserviceBase.Domain.Services;

public class LazyService : ILazyService
{
    private readonly ILogger<LazyService> logger;

    public LazyService(ILogger<LazyService> logger)
    {
        logger.LogInformation("LazyStartupService constructor");
        System.Threading.Thread.Sleep(10000);
        this.logger = logger;
    }
    public void LazyStartupService()
    {
        logger.LogInformation("LazyStartupService");
    }
}

