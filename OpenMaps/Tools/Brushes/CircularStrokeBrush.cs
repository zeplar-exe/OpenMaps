using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenMaps.Common;
using OpenMaps.Controls.Drawing;
using OpenMaps.Tools.Metrics;

namespace OpenMaps.Tools.Brushes;

public class CircularStrokeBrush : StrokeBrush
{
    public IntegerMetric Radius { get; }
    
    public override ImageSource Icon => new BitmapImage();

    public CircularStrokeBrush()
    {
        Radius = new IntegerMetric();
    }

    public override void RegisterMetrics(MetricRegisterBridge register)
    {
        register.Register(Radius);
    }
    
    public override void DrawAt(IntVector position, PixelDisplayBridge display)
    {
        for (var x = position.X - Radius.Value ; x <= position.X; x++)
        {
            for (var y = position.Y - Radius.Value ; y <= position.Y; y++)
            {
                if ((x - position.X) * (x - position.X) + (y - position.Y) * (y - position.Y) <= Radius.Value * Radius.Value) 
                {
                    var xSym = position.X - (x - position.X);
                    var ySym = position.Y - (y - position.Y);

                    if (x < display.DisplaySize.X && x >= 0)
                    {
                        if (y < display.DisplaySize.Y && y >= 0)
                        {
                            display[x, y] = Color;
                        }

                        if (ySym < display.DisplaySize.Y && ySym >= 0)
                        {
                            display[x, ySym] = Color;
                        }
                    }
                    
                    if (xSym < display.DisplaySize.X && xSym >= 0)
                    {
                        if (y < display.DisplaySize.Y && y >= 0)
                        {
                            display[xSym, y] = Color;
                        }

                        if (ySym < display.DisplaySize.Y && y >= 0)
                        {
                            display[xSym, ySym] = Color;
                        }
                    }
                }
            }
        } // Funny math, https://stackoverflow.com/questions/15856411/
    }
}