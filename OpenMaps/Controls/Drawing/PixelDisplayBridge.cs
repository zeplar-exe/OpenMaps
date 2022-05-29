using System.Windows;
using OpenMaps.Common;

namespace OpenMaps.Controls.Drawing;

public class PixelDisplayBridge
{
    private IPixelDisplay PixelDisplay { get; }
    public IntVector DisplaySize => PixelDisplay.Size;

    public PixelDisplayBridge(IPixelDisplay pixelDisplay)
    {
        PixelDisplay = pixelDisplay;
    }

    public IColorPixel this[int x, int y]
    {
        get => PixelDisplay[x, y];
        set => PixelDisplay[x, y] = value;
    }
    public IColorPixel[,] GetRect(Int32Rect rect) => PixelDisplay.GetRect(rect);
    public void SetRect(Int32Rect rect, IColorPixel value) => PixelDisplay.SetRect(rect, value);
}