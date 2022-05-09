using System.Text;

namespace OpenMaps.Common.OMM;

public class MapDataContainer
{
    private static Encoding Encoding => Encoding.UTF8;
    
    public int Width { get; set; }
    public int Height { get; set; }
    public List<MapModeDescription> MapModes { get; }
    public byte[] MissingColorBytes { get; set; }

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

    public MapDataContainer WithArgbWhiteMissingColor()
    {
        MissingColorBytes = new byte[] { 255, 255, 255 };

        return this;
    }
    
    public MapDataContainer ImportStream(Stream input)
    {
        using var reader = new BinaryReader(input);

        var magic = CreateMagicNumber();
        var magicBuffer = new byte[magic.Length];

        if (reader.Read(magicBuffer) != magicBuffer.Length)
            return this;

        if (!magic.SequenceEqual(magicBuffer))
            return this;

        var width = reader.ReadInt32();
        var height = reader.ReadInt32();

        var modeCount = reader.ReadInt32();
        var modes = new List<MapModeDescription>();

        for (var modeIndex = 0; modeIndex < modeCount; modeIndex++)
        {
            var modeNameLength = reader.ReadInt32();
            var modeNameBuffer = new byte[modeNameLength];

            if (reader.Read(modeNameBuffer) < modeNameLength)
                return this;

            var layerCount = reader.ReadInt32();
            var layers = new List<LayerDescription>();

            for (var layerIndex = 0; layerIndex < layerCount; layerIndex++)
            {
                var layerNameLength = reader.ReadInt32();
                var layerNameBuffer = new byte[layerNameLength];

                if (reader.Read(layerNameBuffer) < layerNameLength)
                    return this;

                var byteBuffer = new byte[width * height];

                if (reader.Read(byteBuffer) < width * height)
                    return this;
                
                layers.Add(new LayerDescription(Encoding.GetString(layerNameBuffer), byteBuffer));
            }
            
            modes.Add(new MapModeDescription(Encoding.GetString(modeNameBuffer), layers));
        }

        return this
            .WithSize(width, height)
            .WithModes(modes);
    }

    public void Export(Stream output)
    {
        using var writer = new BinaryWriter(output);
        
        writer.Write(CreateMagicNumber());
        
        writer.Write(Width);
        writer.Write(Height);

        writer.Write(MapModes.Count);
        
        foreach (var mode in MapModes)
        {
            var nameBytes = Encoding.GetBytes(mode.Name);
            
            writer.Write(nameBytes.Length);
            writer.Write(nameBytes);
            
            writer.Write(mode.Layers.Count);

            foreach (var layer in mode.Layers)
            {
                nameBytes = Encoding.GetBytes(layer.Name);
                
                writer.Write(nameBytes.Length);
                writer.Write(nameBytes);

                if (layer.Bytes.Length != Width * Height)
                    throw new MapExportException(
                        "A layer cannot cannot have a different byte representation than the owner map's. " +
                        $"Expected {Width} * {Height} ({Width * Height}). Got {layer.Bytes.Length}");
                
                writer.Write(layer.Bytes);
            }
        }
    }
    
    private static byte[] CreateMagicNumber() => Encoding.GetBytes("OMM_BIN");
}