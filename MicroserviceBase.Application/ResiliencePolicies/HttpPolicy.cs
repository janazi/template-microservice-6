using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace MicroserviceBase.Application.ResiliencePolicies;

public class HttpPolicy
{
    public AsyncRetryPolicy AsyncRetryPolicy { get; }
    public AsyncCircuitBreakerPolicy AsyncCircuitBreakerPolicy { get; }

    public HttpPolicy()
    {
        AsyncRetryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(new[]
            {
                    TimeSpan.FromMilliseconds(10),
                    TimeSpan.FromMilliseconds(20),
                    TimeSpan.FromMilliseconds(40),
            });

        AsyncCircuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(15));
    }
}
