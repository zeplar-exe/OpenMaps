using System;

namespace OpenMaps.Common;

public static class Math2
{
    public static int FloorToInt(float f) => (int)f;
    public static int FloorToInt(double f) => (int)f;
    public static int FloorToInt(decimal f) => (int)f;
    
    public static int CeilToInt(float f) => (int)Math.Ceiling(f);
    public static int CeilToInt(double f) => (int)Math.Ceiling(f);
    public static int CeilToInt(decimal f) => (int)Math.Ceiling(f);
}