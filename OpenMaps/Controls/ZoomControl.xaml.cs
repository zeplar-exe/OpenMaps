using System.Windows.Controls;
using System.Windows.Media;
using OpenMaps.MVVM;

namespace OpenMaps.Controls;

public partial class ZoomControl : UserControl
{
    private ScaleTransform ScaleTransform { get; set; }
    
    public ObservableProperty<double> Zoom { get; }
    public ObservableProperty<double> DefaultZoomOffset { get; }

    public ZoomControl()
    {
        Zoom = new ObservableProperty<double>(1);
        DefaultZoomOffset = new ObservableProperty<double>(0.2d);
        
        InitializeComponent();

        LayoutTransform = new ScaleTransform(Zoom.Value, Zoom.Value);
        
        Zoom.ValueChanged += (_, i) =>
        {
            LayoutTransform = new ScaleTransform(i, i);
        };
        // the zoom control doesn't detect mouse event that isn;t on children
    }

    public void ZoomIn()
    {
        ZoomIn(DefaultZoomOffset.Value);
    }

    public void ZoomIn(double offset)
    {
        Zoom.Value += offset;
    }

    public void ZoomOut()
    {
        ZoomOut(DefaultZoomOffset.Value);
    }

    public void ZoomOut(double offset)
    {
        Zoom.Value -= offset;
    }
}