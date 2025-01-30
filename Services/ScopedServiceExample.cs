namespace SecondPhase.Services;

public class ScopedServiceExample(ILogger<ScopedServiceExample> logger) : IScopedServiceExample
{
    private readonly ILogger _logger = logger;
    private readonly Guid _serviceGuid = Guid.NewGuid();

    public void LogMessageGuid() => _logger.LogInformation("Service Guid: {serviceGuid}", _serviceGuid);
}