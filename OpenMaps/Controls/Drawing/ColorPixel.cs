using System.Windows.Media;

namespace OpenMaps.Controls.Drawing;

public class ColorPixel : IColorPixel
{
    private byte A { get; }
    private byte R { get; }
    private byte G { get; }
    private byte B { get; }

    public ColorPixel(byte a, byte r, byte g, byte b)
    {
        A = a;
        R = r;
        G = g;
        B = b;
    }

    public ColorPixel(Color color)
    {
        A = color.A;
        R = color.R;
        G = color.G;
        B = color.B;
    }

    public byte GetA()
    {
        return A;
    }

    public byte GetR()
    {
        return R;
    }

    public byte GetG()
    {
        return G;
    }

    public byte GetB()
    {
        return B;
    }
}