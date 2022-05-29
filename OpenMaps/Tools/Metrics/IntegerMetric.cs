namespace OpenMaps.Tools.Metrics;

public class IntegerMetric : IMetric
{
    public int Value { get; set; }

    public bool TrySet(string input)
    {
        var result = int.TryParse(input, out var i);

        if (result)
            Value = i;

        return result;
    }
}