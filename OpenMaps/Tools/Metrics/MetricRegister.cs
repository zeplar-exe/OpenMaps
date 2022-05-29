using System;
using System.Collections.Generic;

namespace OpenMaps.Tools.Metrics;

public class MetricRegister
{
    public List<IMetric> RegisteredMetrics { get; }

    public MetricRegister()
    {
        RegisteredMetrics = new List<IMetric>();
    }

    public void Register(IMetric metric)
    {
        ArgumentNullException.ThrowIfNull(metric);
        
        RegisteredMetrics.Add(metric);
    }
}