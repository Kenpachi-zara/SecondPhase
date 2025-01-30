namespace SecondPhase.Services;

public class TransientDependencyB(ITransientServiceExample transientServiceExample) : ITransientDependencyB
{
    public void LogMessageGuid() => transientServiceExample.LogMessageGuid();
}