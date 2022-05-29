using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenMaps.Common;
using OpenMaps.Controls.Drawing;
using OpenMaps.Tools.Metrics;

namespace OpenMaps.Tools.Brushes;

public class RectangularStokeBrush : StrokeBrush
{
    public IColorPixel StrokeColor { get; set; }
    
    public IntegerMetric StrokeWidth { get; }
    public IntegerMetric Width { get; }
    public IntegerMetric Height { get; }

    public RectangularStokeBrush()
    {
        StrokeColor = new ColorPixel(Colors.Black);
        
        StrokeWidth = new IntegerMetric();
        Width = new IntegerMetric();
        Height = new IntegerMetric();
    }
    
    public override ImageSource Icon => new BitmapImage();
    
    public override void RegisterMetrics(MetricRegisterBridge register)
    {
        register.Register(StrokeWidth);
        register.Register(Width);
        register.Register(Height);
    }

    public override void DrawAt(IntVector position, PixelDisplayBridge display)
    {
        var topLeft = new IntVector(
            Math.Clamp(position.X - Math2.CeilToInt(Width.Value / 2d), 0, display.DisplaySize.X),
            Math.Clamp(position.Y - Math2.CeilToInt(Height.Value / 2d), 0, display.DisplaySize.Y));
        
        for (var x = topLeft.X; x < topLeft.X + Width.Value && x < display.DisplaySize.X; x++)
        {
            for (var y = topLeft.Y; y < topLeft.Y + Height.Value && y < display.DisplaySize.Y; y++)
            {
                if ((x <= topLeft.X + StrokeWidth.Value 
                     || x >= topLeft.X + Width.Value - StrokeWidth.Value - 1)
                    || (y <= topLeft.Y + StrokeWidth.Value
                        || y >= topLeft.Y + Height.Value - StrokeWidth.Value - 1))
                {
                    display[x, y] = StrokeColor;
                }
                else
                {
                    display[x, y] = Color;
                }
            }
        }
    }
}