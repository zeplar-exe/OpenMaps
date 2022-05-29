using System;
using System.Windows.Media;
using OpenMaps.Common;
using OpenMaps.Controls.Drawing;

namespace OpenMaps.Tools.Brushes;

public abstract class StrokeBrush : Tool
{
    private IntVector LastPosition { get; set; }

    public IColorPixel Color { get; set; }

    protected StrokeBrush()
    {
        Color = new ColorPixel(Colors.Black);
    }

    public override void OnCanvasStrokeBegin(IntVector position, PixelDisplayBridge display)
    {
        LastPosition = position;

        base.OnCanvasStrokeBegin(position, display);
    }

    public override void OnCanvasStroke(IntVector position, PixelDisplayBridge display)
    {
        display[position.X, position.Y] = Color;

        if (LastPosition != position)
        {
            var alphaAdd = 1d / new IntVector(position.X - LastPosition.X, position.Y - LastPosition.Y).Magnitude;
            
            if (Math.Abs(alphaAdd - 1) < 0.00000001)
                goto ExitInterpolate;
            
            var alpha = 0d;

            var xDiff = position.X - LastPosition.X;
            var yDiff = position.Y - LastPosition.Y;

            while (alpha < 1d)
            {
                alpha += alphaAdd;

                var adjusted = new IntVector(
                    Math2.FloorToInt(position.X + xDiff * alpha),
                    Math2.FloorToInt(position.Y + yDiff * alpha));

                DrawAt(adjusted, display);
            }
        }
        
        ExitInterpolate:

        DrawAt(position, display);

        base.OnCanvasStroke(position, display);
    }
    
    public abstract void DrawAt(IntVector position, PixelDisplayBridge display);
}