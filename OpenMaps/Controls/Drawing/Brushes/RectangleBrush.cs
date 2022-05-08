using System;
using System.Windows.Media;
using OpenMaps.Common;

namespace OpenMaps.Controls.Drawing.Brushes;

public class RectangleBrush : IDrawBrush
{
    public IColorPixel FillColor { get; set; }
    public int StrokeWidth { get; set; }
    public IColorPixel StrokeColor { get; set; }
    public IntVector Size { get; set; }
    
    public RectangleBrush(IColorPixel fillColor, IntVector size)
    {
        FillColor = fillColor;
        StrokeColor = new ColorPixel(Colors.Black);
        Size = size;
    }

    public void Draw(IntVector position, IPixelDisplay display)
    {
        var topLeft = new IntVector(
            Math.Clamp(position.X - Math2.CeilToInt(Size.X / 2d), 0, display.Size.X),
            Math.Clamp(position.Y - Math2.CeilToInt(Size.Y / 2d), 0, display.Size.Y));
        
        for (var x = topLeft.X; x < topLeft.X + Size.X && x < display.Size.X; x++)
        {
            for (var y = topLeft.Y; y < topLeft.Y + Size.Y && y < display.Size.Y; y++)
            {
                if ((x <= topLeft.X + StrokeWidth 
                    || x >= topLeft.X + Size.X - StrokeWidth - 1)
                    || (y <= topLeft.Y + StrokeWidth
                    || y >= topLeft.Y + Size.Y - StrokeWidth - 1))
                {
                    display[x, y] = StrokeColor;
                }
                else
                {
                    display[x, y] = FillColor;
                }
            }
        }
    }
}