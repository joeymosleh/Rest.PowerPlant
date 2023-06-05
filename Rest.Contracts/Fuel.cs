using System.Text.Json.Serialization;

namespace Rest.Contracts;

public class Fuel
{
    [JsonPropertyName("gas(euro/MWh)")]
    public double GasPrice { get; set; }

    [JsonPropertyName("kerosine(euro/MWh)")]
    public double KerosinePrice { get; set; }

    [JsonPropertyName("co2(euro/ton)")]
    public double Co2EmissionPrice { get; set; }

    [JsonPropertyName("wind(%)")]
    public int WindPercentage { get; set; }
}