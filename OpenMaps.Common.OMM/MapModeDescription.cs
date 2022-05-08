namespace OpenMaps.Common.OMM;

public class MapModeDescription
{
    public string Name { get; set; }
    public List<LayerDescription> Layers { get; }
    
    public MapModeDescription(string name)
    {
        Name = name;
        Layers = new List<LayerDescription>();
    }
}