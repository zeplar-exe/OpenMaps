using OpenMaps.Common;

namespace OpenMaps.Controls.Drawing.Brushes;

public interface IDrawBrush
{
    public void Draw(IntVector position, IPixelDisplay display);
}