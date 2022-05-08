using System.Windows.Media;
using OpenMaps.Common;

namespace OpenMaps.Controls.Drawing.Brushes;

public class CircleBrush : IDrawBrush
{
    public IColorPixel Color { get; set; }
    public int Radius { get; set; }
    
    public CircleBrush()
    {
        Color = new ColorPixel(Colors.Black);
    }
    
    public CircleBrush(IColorPixel color, int radius)
    {
        Color = color;
        Radius = radius;
    }
    
    public void Draw(IntVector position, IPixelDisplay display)
    {
        for (var x = position.X - Radius ; x <= position.X; x++)
        {
            for (var y = position.Y - Radius ; y <= position.Y; y++)
            {
                if ((x - position.X) * (x - position.X) + (y - position.Y) * (y - position.Y) <= Radius * Radius) 
                {
                    var xSym = position.X - (x - position.X);
                    var ySym = position.Y - (y - position.Y);

                    if (x < display.Size.X && x >= 0)
                    {
                        if (y < display.Size.Y && y >= 0)
                        {
                            display[x, y] = Color;
                        }

                        if (ySym < display.Size.Y && ySym >= 0)
                        {
                            display[x, ySym] = Color;
                        }
                    }
                    
                    if (xSym < display.Size.X && xSym >= 0)
                    {
                        if (y < display.Size.Y && y >= 0)
                        {
                            display[xSym, y] = Color;
                        }

                        if (ySym < display.Size.Y && y >= 0)
                        {
                            display[xSym, ySym] = Color;
                        }
                    }
                }
            }
        } // Funny math, https://stackoverflow.com/questions/15856411/
    }
}