using OpenMaps.Tools;
using OpenMaps.Tools.Metrics;

namespace OpenMaps.MVVM;

public class ToolObject
{
    public Tool Tool { get; }
    public MetricRegister MetricRegister { get; }
    
    public ToolObject(Tool tool)
    {
        Tool = tool;
        MetricRegister = new MetricRegister();
        
        Tool.RegisterMetrics(new MetricRegisterBridge(MetricRegister));
    }
}