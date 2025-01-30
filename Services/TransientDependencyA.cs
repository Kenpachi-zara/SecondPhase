namespace SecondPhase.Services;

public class TransientDependencyA(ITransientServiceExample transientServiceExample) : ITransientDependencyA
{
    public void LogMessageGuid() => transientServiceExample.LogMessageGuid();
}