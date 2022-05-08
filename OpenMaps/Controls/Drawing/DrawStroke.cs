using System.Collections.Generic;
using System.Windows.Controls;
using OpenMaps.Common;

namespace OpenMaps.Controls.Drawing;

public class DrawStroke
{
    public List<IntVector> Points { get; }

    public DrawStroke()
    {
        Points = new List<IntVector>();
    }
}