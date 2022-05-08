namespace OpenMaps.Common.OMM;

public class MapDataContainer
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<MapModeDescription> MapModes { get; }

    public MapDataContainer()
    {
        MapModes = new List<MapModeDescription>();
    }

    public MapDataContainer WithSize(int width, int height)
    {
        Width = width;
        Height = height;

        return this;
    }

    public MapDataContainer WithModes(IEnumerable<MapModeDescription> modes)
    {
        MapModes.Clear();
        MapModes.AddRange(modes);

        return this;
    }
    
    public MapDataContainer WithModes(params MapModeDescription[] modes)
    {
        MapModes.Clear();
        MapModes.AddRange(modes);

        return this;
    }
    
    public MapDataContainer ImportStream(Stream input)
    {
        using var reader = new BinaryReader(input);

        return this;
    }

    public void Export(Stream output)
    {
        using var writer = new BinaryWriter(output);
    }
}