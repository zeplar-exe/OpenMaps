using System.Windows.Media;
using OpenMaps.Common;
using OpenMaps.Controls.Drawing;
using OpenMaps.Tools.Metrics;

namespace OpenMaps.Tools;

public abstract class Tool
{
    public abstract ImageSource Icon { get; }
    
    public abstract void RegisterMetrics(MetricRegisterBridge register);
    
    public virtual void OnCanvasStrokeBegin(IntVector position, PixelDisplayBridge display) { }
    public virtual void OnCanvasStroke(IntVector position, PixelDisplayBridge display) { }
    public virtual void OnCanvasStrokeEnd(IntVector position, PixelDisplayBridge display) { }
}