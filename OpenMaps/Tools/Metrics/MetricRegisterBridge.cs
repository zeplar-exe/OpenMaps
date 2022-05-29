namespace OpenMaps.Tools.Metrics;

public class MetricRegisterBridge
{
    private MetricRegister InternalRegister { get; }

    public MetricRegisterBridge(MetricRegister register)
    {
        InternalRegister = register;
    }

    public void Register(IMetric metric)
    {
        InternalRegister.Register(metric);
    }
}