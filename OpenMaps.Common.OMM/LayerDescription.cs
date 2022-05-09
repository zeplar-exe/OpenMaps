namespace OpenMaps.Common.OMM;

public class LayerDescription
{
    public string Name { get; set; }
    public byte[] Bytes { get; set; }

    public LayerDescription(string name, IEnumerable<byte> bytes)
    {
        Name = name;
        Bytes = bytes.ToArray();
    }
}