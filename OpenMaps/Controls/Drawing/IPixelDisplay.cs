using System.Windows;
using System.Windows.Media;
using OpenMaps.Common;

namespace OpenMaps.Controls.Drawing;

public interface IPixelDisplay
{
    public IntVector Size { get; }
    
    public IColorPixel this[int x, int y] { get; set; }

    public void Lock();
    public void Unlock();

    public IColorPixel[,] GetRect(Int32Rect rect);
    public void SetRect(Int32Rect rect, IColorPixel value);
    
    public void Draw(DrawingContext context, Rect rect);
}