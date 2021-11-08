using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace MicroserviceBase.Application.ResiliencePolicies;

public class RedisPolicy
{
    public AsyncRetryPolicy AsyncRetryPolicy { get; }
    public AsyncCircuitBreakerPolicy AsyncCircuitBreakerPolicy { get; }

    public RedisPolicy()
    {
        AsyncRetryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(new[]
            {
                    TimeSpan.FromMilliseconds(50),
                    TimeSpan.FromMilliseconds(150),
                    TimeSpan.FromMilliseconds(450),
            });

        AsyncCircuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
    }
}
