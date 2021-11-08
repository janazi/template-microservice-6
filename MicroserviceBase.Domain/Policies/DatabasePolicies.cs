using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace MicroserviceBase.Domain.Policies;

public class DatabasePolicies
{
    public AsyncRetryPolicy RetryPolicy { get; }
    public AsyncCircuitBreakerPolicy AsyncCircuitBreakerPolicy { get; }

    public DatabasePolicies()
    {
        RetryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(new[]
            {
                    TimeSpan.FromMilliseconds(50),
                    TimeSpan.FromMilliseconds(150),
                    TimeSpan.FromMilliseconds(450)
            });

        AsyncCircuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(15));
    }
}
