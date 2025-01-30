using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SecondPhase.Models;
using SecondPhase.Services;

namespace SecondPhase.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController(
    ILogger<HomeController> logger,
    IScopedServiceExample scopedService, 
    ISingletonServiceExample singletonService, 
    ITransientDependencyA transientDependencyA,
    ITransientDependencyB transientDependencyB) : ControllerBase
{
    private readonly ILogger _logger = logger;

    [Route("scoped")]
    public IActionResult Scoped()
    {
        _logger.LogInformation("Scoped request started");
        scopedService.LogMessageGuid();
        _logger.LogInformation("Scoped request ended");
        return Ok();
    }
    
    [Route("singleton")]
    public IActionResult Singleton()
    {
        _logger.LogInformation("Singleton request started");
        singletonService.LogMessageGuid();
        _logger.LogInformation("Singleton request ended");
        return Ok();
    }
    
    [Route("transient")]
    public IActionResult Transient()
    {
        _logger.LogInformation("Transient request started");
        transientDependencyA.LogMessageGuid();
        transientDependencyB.LogMessageGuid();
        _logger.LogInformation("Transient request ended");
        return Ok();
    }
}
