namespace SecondPhase.Services;

public class SingletonServiceExample(ILogger<SingletonServiceExample> logger) : ISingletonServiceExample
{
    private readonly ILogger _logger = logger;
    private readonly Guid _serviceGuid = Guid.NewGuid();
    
    public void LogMessageGuid() => _logger.LogInformation("Service Guid: {serviceGuid}", _serviceGuid);
}