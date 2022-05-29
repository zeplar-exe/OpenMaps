namespace OpenMaps.Tools.Metrics;

public class DecimalMetric : IMetric
{
    public decimal Value { get; set; }
    
    public bool TrySet(string input)
    {
        var result = decimal.TryParse(input, out var d);

        if (result)
            Value = d;
        
        return result;
    }
}