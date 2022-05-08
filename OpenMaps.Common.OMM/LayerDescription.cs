namespace OpenMaps.Common.OMM;

public class LayerDescription
{
    public string Name { get; }
    public byte[] Bytes { get; }
    
    public LayerDescription(string name, IEnumerable<byte> bytes)
    {
        Name = name;
        Bytes = bytes.ToArray();
    }
}